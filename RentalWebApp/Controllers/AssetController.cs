using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentalWebApp.Models.Entities;
using RentalWebApp.Models.RequestModels;
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
                string query = @"SELECT AssetId, AssetCode, Category.CategoryName, Quantity, AssetName, AssetStatus, CreateDate, Asset.IsActive
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
                string duplicateTestingQuery = @"SELECT [AssetId]
      ,[CategoryId]
      ,[AssetCode]
      ,[AssetName]
      ,[AssetStatus]
      ,[CreateDate]
      ,[IsActive]
  FROM [dbo].[Asset] WHERE AssetCode = @AssetCode AND IsActive = @IsActive";
                List<SqlParameter> sqlParameters = new()
                {
                    new("@AssetCode", dataModel.AssetCode),
                    new("@IsActive", true)
                };
                DataTable asset = DbHelper.Query(duplicateTestingQuery, sqlParameters.ToArray());
                if (asset.Rows.Count > 0)
                {
                    TempData["error"] = "Asset Code already exists!";
                    return RedirectToAction("AssetManagement");
                }

                string query = @"INSERT INTO Asset (AssetCode, CategoryId, AssetName, AssetStatus, Quantity, CreateDate, IsActive)
VALUES (@AssetCode, @CategoryId, @AssetName, @AssetStatus, @Quantity, @CreateDate, @IsActive)";
                List<SqlParameter> parameters = new()
                {
                    new("@CategoryId", dataModel.CategoryId),
                    new("@AssetCode", dataModel.AssetCode),
                    new("@AssetName", dataModel.AssetName),
                    new("@AssetStatus", dataModel.AssetStatus),
                    new("@Quantity", dataModel.Quantity),
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

        public IActionResult EditAsset(long id)
        {
            try
            {
                string query = @"SELECT [AssetId]
      ,[CategoryId]
      ,[AssetCode]
      ,[AssetName]
      ,[AssetStatus]
      ,[Quantity]
      ,[CreateDate]
      ,[IsActive]
  FROM [dbo].[Asset] WHERE AssetId = @AssetId AND IsActive = @IsActive";
                List<SqlParameter> parameters = new()
                {
                    new("@AssetId", id),
                    new("@IsActive", true)
                };
                DataTable asset = DbHelper.Query(query, parameters.ToArray());
                string assetJson = JsonConvert.SerializeObject(asset);
                AssetDataModel assetDataModel = new()
                {
                    AssetId = Convert.ToInt64(asset.Rows[0]["AssetId"]),
                    CategoryId = Convert.ToInt64(asset.Rows[0]["CategoryId"]),
                    AssetCode = Convert.ToString(asset.Rows[0]["AssetCode"])!,
                    AssetName = Convert.ToString(asset.Rows[0]["AssetName"])!,
                    AssetStatus = Convert.ToString(asset.Rows[0]["AssetStatus"])!,
                    Quantity = Convert.ToInt32(asset.Rows[0]["Quantity"])
                };


                string query1 = @"SELECT [CategoryId]
      ,[CategoryName]
      ,[IsActive]
  FROM [dbo].[Category] WHERE IsActive = @IsActive";
                DataTable category = DbHelper.Query(query1, new SqlParameter("@IsActive", true));
                string categoryJson = JsonConvert.SerializeObject(category);
                List<CategoryDataModel> categories = JsonConvert.DeserializeObject<List<CategoryDataModel>>(categoryJson)!;

                EditAssetResponseModel respModel = new()
                {
                    Categories = categories,
                    AssetDataModel = assetDataModel
                };

                return View(respModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Update(UpdateAssetRequestModel requestModel)
        {
            try
            {
                string duplicateTestingQuery = @"SELECT [AssetId]
      ,[CategoryId]
      ,[AssetCode]
      ,[AssetName]
      ,[AssetStatus]
      ,[CreateDate]
      ,[IsActive]
  FROM [dbo].[Asset] WHERE AssetCode = @AssetCode AND IsActive = @IsActive AND AssetId != @AssetId";
                List<SqlParameter> sqlParameters = new()
                {
                    new("@AssetId", requestModel.AssetId),
                    new("@AssetCode", requestModel.AssetCode),
                    new("@IsActive", true)
                };
                DataTable asset = DbHelper.Query(duplicateTestingQuery, sqlParameters.ToArray());
                if (asset.Rows.Count > 0)
                {
                    TempData["error"] = "Asset Code already exists!";
                    return RedirectToAction("AssetManagement");
                }


                string query = @"UPDATE Asset SET CategoryId = @CategoryId, AssetCode = @AssetCode, AssetName = @AssetName,
AssetStatus = @AssetStatus, Quantity = @Quantity WHERE AssetId = @AssetId";
                List<SqlParameter> parameters = new()
                {
                    new("@CategoryId", requestModel.CategoryId),
                    new("@AssetCode", requestModel.AssetCode),
                    new("@AssetName", requestModel.AssetName),
                    new("@AssetStatus", requestModel.AssetStatus),
                    new("@Quantity", requestModel.Quantity),
                    new("@AssetId", requestModel.AssetId)
                };
                int result = DbHelper.Execute(query, parameters.ToArray());

                if (result > 0)
                {
                    TempData["success"] = "Updating Successful!";
                }
                else
                {
                    TempData["error"] = "Updating Fail!";
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
