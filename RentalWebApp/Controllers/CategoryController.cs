using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentalWebApp.Models.Entities;
using RentalWebApp.Models.RequestModels;
using RentalWebApp.Services;
using System.Data;
using System.Data.SqlClient;

namespace RentalWebApp.Controllers
{
    public class CategoryController : Controller
    {
        public readonly IConfiguration _configuration;

        public CategoryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult CategoryManagement()
        {
            try
            {
                SqlConnection conn = new(_configuration.GetConnectionString("DbConnection"));
                conn.Open();
                string query = @"SELECT [CategoryId]
      ,[CategoryName]
      ,[IsActive]
  FROM [dbo].[Category] WHERE IsActive = @IsActive";
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@IsActive", true);
                SqlDataAdapter adapter = new(cmd);
                DataTable dt = new();
                adapter.Fill(dt);
                conn.Close();

                string jsonStr = JsonConvert.SerializeObject(dt);
                List<CategoryDataModel> lst = JsonConvert.DeserializeObject<List<CategoryDataModel>>(jsonStr)!;

                return View(lst);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryRequestModel requestModel)
        {
            try
            {
                string duplicateTestingQuery = @"SELECT [CategoryId]
      ,[CategoryName]
      ,[IsActive]
  FROM [dbo].[Category] WHERE CategoryName = @CategoryName AND IsActive = @IsActive";
                List<SqlParameter> parameters = new()
                {
                    new("@CategoryName", requestModel.CategoryName),
                    new("@IsActive", true)
                };
                DataTable category = IsDuplicate(duplicateTestingQuery, parameters.ToArray());
                if (category.Rows.Count > 0)
                {
                    TempData["error"] = "Category Name already exists!";
                    return RedirectToAction("CategoryManagement");
                }

                // create case
                string query = @"INSERT INTO Category (CategoryName, IsActive) VALUES (@CategoryName, @IsActive)";
                List<SqlParameter> createParams = new()
                {
                    new("@CategoryName", requestModel.CategoryName),
                    new("@IsActive", true)
                };
                int result = DbHelper.Execute(query, createParams.ToArray());

                if (result > 0)
                {
                    TempData["success"] = "Creating Successful!";
                }
                else
                {
                    TempData["error"] = "Creating Fail!";
                }
                return RedirectToAction("CategoryManagement");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IActionResult EditCategory(long id)
        {
            try
            {
                SqlConnection conn = new(_configuration.GetConnectionString("DbConnection"));
                conn.Open();
                string query = @"SELECT [CategoryId]
      ,[CategoryName]
      ,[IsActive]
  FROM [dbo].[Category] WHERE CategoryId = @CategoryId AND IsActive = @IsActive";
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@CategoryId", id);
                cmd.Parameters.AddWithValue("@IsActive", true);
                SqlDataAdapter adapter = new(cmd);
                DataTable dt = new();
                adapter.Fill(dt);
                conn.Close();

                CategoryDataModel dataModel = new()
                {
                    CategoryId = Convert.ToInt64(dt.Rows[0]["CategoryId"]),
                    CategoryName = Convert.ToString(dt.Rows[0]["CategoryName"])!
                };

                return View(dataModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Update(UpdateCategoryRequestModel requestModel)
        {
            try
            {
                string duplicateTestingQuery = @"SELECT [CategoryId]
      ,[CategoryName]
      ,[IsActive]
  FROM [dbo].[Category] WHERE CategoryName = @CategoryName AND IsActive = @IsActive AND CategoryId != @CategoryId";
                List<SqlParameter> parameters = new()
                {
                    new("@CategoryName", requestModel.CategoryName),
                    new("@IsActive", true),
                    new("@CategoryId", requestModel.CategoryId)
                };
                DataTable category = IsDuplicate(duplicateTestingQuery, parameters.ToArray());
                if (category.Rows.Count > 0)
                {
                    TempData["error"] = "Category Name already exists!";
                    return RedirectToAction("CategoryManagement");
                }

                // update case
                string query = @"UPDATE Category SET CategoryName = @CategoryName
WHERE CategoryId = @CategoryId";
                List<SqlParameter> updateParams = new()
                {
                    new("@CategoryName", requestModel.CategoryName),
                    new("@CategoryId", requestModel.CategoryId)
                };
                int result = DbHelper.Execute(query, updateParams.ToArray());

                if (result > 0)
                {
                    TempData["success"] = "Updating Successful!";
                }
                else
                {
                    TempData["error"] = "Updating Fail!";
                }

                return RedirectToAction("CategoryManagement");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IActionResult Delete(long id)
        {
            try
            {
                string query1 = @"SELECT AssetId, CategoryId, AssetName FROM Asset WHERE CategoryId = @CategoryId";
                DataTable dt1 = DbHelper.Query(query1, new SqlParameter("@CategoryId", id));
                if (dt1.Rows.Count > 0)
                {
                    TempData["error"] = "Cannot Delete!";
                    return RedirectToAction("CategoryManagement");
                }

                string query = @"UPDATE Category SET IsActive = @IsActive
WHERE CategoryId = @CategoryId";
                List<SqlParameter> deleteParams = new()
                {
                    new("@IsActive", false),
                    new("@CategoryId", id)
                };
                int result = DbHelper.Execute(query, deleteParams.ToArray());

                if (result > 0)
                {
                    TempData["success"] = "Deleting Successful!";
                }
                else
                {
                    TempData["error"] = "Deleting Fail!";
                }
                return RedirectToAction("CategoryManagement");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable IsDuplicate(string query, params SqlParameter[] sqlParameters)
        {
            try
            {
                SqlConnection conn = new(_configuration.GetConnectionString("DbConnection"));
                conn.Open();
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddRange(sqlParameters);
                SqlDataAdapter adapter = new(cmd);
                DataTable dt = new();
                adapter.Fill(dt);
                conn.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
