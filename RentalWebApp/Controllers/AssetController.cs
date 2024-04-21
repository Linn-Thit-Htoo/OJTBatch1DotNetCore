using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentalWebApp.Models.Entities;
using RentalWebApp.Models.ResponseModels;
using RentalWebApp.Services;
using System.Data;
using System.Data.SqlClient;

namespace RentalWebApp.Controllers
{
    public class AssetController : Controller
    {
        public IActionResult AssetManagement()
        {
            try
            {
                string query = @"SELECT AssetId, Category.CategoryName, AssetName, AssetStatus, CreateDate, Asset.IsActive
FROM Asset
INNER JOIN Category ON Asset.CategoryId = Category.CategoryId
WHERE Asset.IsActive = @IsActive AND Category.IsActive = @IsActive";
                DataTable dt = DbHelper.Query(query, sqlParameters: new SqlParameter("@IsActive", true));

                string jsonStr = JsonConvert.SerializeObject(dt);
                List<AssetResponseModel> lst = JsonConvert.DeserializeObject<List<AssetResponseModel>>(jsonStr)!;

                return View(lst);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IActionResult CreateAsset()
        {
            try
            {
                string query = @"SELECT [CategoryId]
      ,[CategoryName]
      ,[IsActive]
  FROM [dbo].[Category] WHERE IsActive = @IsActive";
                DataTable dt = DbHelper.Query(query, new SqlParameter("@IsActive", true));

                string jsonStr = JsonConvert.SerializeObject(dt);
                List<CategoryDataModel> lst = JsonConvert.DeserializeObject<List<CategoryDataModel>>(jsonStr)!;

                return View(lst);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Save(AssetDataModel dataModel)
        {
            try
            {
                string query = @"INSERT INTO Asset (CategoryId, AssetName, AssetStatus, CreateDate, IsActive)
VALUES (@CategoryId, @AssetName, @AssetStatus, @CreateDate, @IsActive)";
                List<SqlParameter> parameters = new()
                {
                    new("@CategoryId", dataModel.CategoryId),
                    new("@AssetName", dataModel.AssetName),
                    new("@AssetStatus", dataModel.AssetStatus),
                    new("@CreateDate", DateTime.Now),
                    new("@IsActive", true)
                };

                int result = DbHelper.Execute(query, parameters.ToArray());

                if (result > 0)
                {
                    TempData["success"] = "Creating Successful!";
                }
                else
                {
                    TempData["error"] = "Creating Fail!";
                }
                return RedirectToAction("AssetManagement");
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
                string query = @"UPDATE Asset SET IsActive = @IsActive WHERE AssetId = @AssetId";
                List<SqlParameter> parameters = new()
                {
                    new("@AssetId", id),
                    new("@IsActive", false)
                };
                int result = DbHelper.Execute(query, parameters.ToArray());

                if (result > 0)
                {
                    TempData["success"] = "Deleting Successful!";
                }
                else
                {
                    TempData["error"] = "Deleting Fail!";
                }
                return RedirectToAction("AssetManagement");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
