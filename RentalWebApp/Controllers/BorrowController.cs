using Microsoft.AspNetCore.Mvc;
using RentalWebApp.Models.Entities;
using RentalWebApp.Models.RequestModels;
using RentalWebApp.Services;
using System.Data;
using System.Data.SqlClient;

namespace RentalWebApp.Controllers
{
    public class BorrowController : Controller
    {
        public IActionResult BorrowManagement()
        {
            return View();
        }

        public IActionResult CreateBorrow()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(CreateBorrowRequestModel requestModel)
        {
            try
            {
                string userFetchQuery = @"SELECT [UserId]
  FROM [dbo].[Users] WHERE MemberId = @MemberId AND IsActive = @IsActive";
                List<SqlParameter> userFetchParams = new()
                {
                    new("@MemberId", requestModel.MemberId),
                    new("@IsActive", true)
                };
                DataTable user = DbHelper.Query(userFetchQuery, userFetchParams.ToArray());
                if (user.Rows.Count == 0)
                {
                    TempData["error"] = "User does not exist!";
                    return RedirectToAction("BorrowManagement");
                }
                long userID = Convert.ToInt64(user.Rows[0]["UserId"]);

                string assetFetchQuery = @"SELECT AssetId, Quantity
  FROM [dbo].[Asset] WHERE AssetCode = @AssetCode AND IsActive = @IsActive";
                List<SqlParameter> assetFetchParams = new()
                {
                    new("@AssetCode", requestModel.AssetCode),
                    new("@IsActive", true)
                };
                DataTable asset = DbHelper.Query(assetFetchQuery, assetFetchParams.ToArray());
                if (asset.Rows.Count == 0)
                {
                    TempData["error"] = "Asset does not exist!";
                    return RedirectToAction("BorrowManagement");
                }
                long assetID = Convert.ToInt64(asset.Rows[0]["AssetId"]);
                int quantity = Convert.ToInt32(asset.Rows[0]["Quantity"]);

                // reduce
                string reduceQuantityQuery = @"UPDATE Asset SET Quantity = @Quantity WHERE AssetId = @AssetId";
                List<SqlParameter> reduceQuantityQueryParams = new()
                {
                    new("@Quantity", quantity - 1),
                    new("@AssetId", assetID)
                };
                int reduceResult = DbHelper.Execute(reduceQuantityQuery, reduceQuantityQueryParams.ToArray());
                if (reduceResult == 0)
                {
                    TempData["error"] = "Reducing Quantity Fail!";
                    return RedirectToAction("BorrowManagement");
                }

                // insert case
                string insertQuery = @"INSERT INTO Borrow (UserId, AssetId, BorrowDate, ReturnDate, IsActive)
VALUES (@UserId, @AssetId, @BorrowDate, @ReturnDate, @IsActive)";
                List<SqlParameter> insertParams = new()
                {
                    new("@UserId", userID),
                    new("@AssetId", assetID),
                    new("@BorrowDate", DateTime.Now),
                    new("@ReturnDate", string.IsNullOrEmpty(requestModel.ReturnDate) ? DBNull.Value : requestModel.ReturnDate),
                    new("@IsActive", true)
                };
                int insertResult = DbHelper.Execute(insertQuery, insertParams.ToArray());
                if (insertResult > 0)
                {
                    TempData["success"] = "Success!";
                }
                else
                {
                    TempData["error"] = "Fail!";
                }
                return RedirectToAction("BorrowManagement");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
