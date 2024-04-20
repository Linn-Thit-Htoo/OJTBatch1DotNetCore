using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using RentalWebApp.Models.Entities;
using RentalWebApp.Models.RequestModels;
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

                SqlConnection conn = new(_configuration.GetConnectionString("DbConnection"));
                conn.Open();
                string query = @"INSERT INTO Category (CategoryName, IsActive) VALUES (@CategoryName, @IsActive)";
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@CategoryName", requestModel.CategoryName);
                cmd.Parameters.AddWithValue("@IsActive", true);
                int result = cmd.ExecuteNonQuery();
                conn.Close();

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

                SqlConnection conn = new(_configuration.GetConnectionString("DbConnection"));
                conn.Open();
                string query = @"UPDATE Category SET CategoryName = @CategoryName
WHERE CategoryId = @CategoryId";
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@CategoryId", requestModel.CategoryId);
                cmd.Parameters.AddWithValue("@CategoryName", requestModel.CategoryName);
                int result = cmd.ExecuteNonQuery();
                conn.Close();

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
                SqlConnection conn = new(_configuration.GetConnectionString("DbConnection"));
                conn.Open();
                string query = @"UPDATE Category SET IsActive = @IsActive
WHERE CategoryId = @CategoryId";
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@CategoryId", id);
                cmd.Parameters.AddWithValue("@IsActive", false);
                int result = cmd.ExecuteNonQuery();
                conn.Close();

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
