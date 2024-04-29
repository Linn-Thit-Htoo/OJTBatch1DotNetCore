using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentalWebApp.Models.Entities;
using RentalWebApp.Models.RequestModels.Borrow;
using RentalWebApp.Models.ResponseModels;
using RentalWebApp.Services;
using System.Data;
using System.Data.SqlClient;

namespace RentalWebApp.Controllers
{
    public class BorrowController : Controller
    {
        private readonly IConfiguration _configuration;

        public BorrowController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult BorrowManagement()
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("name")))
                {
                    TempData["error"] = "Please login first!";
                    return RedirectToAction("LoginPage", "User");
                }

                string query = @"SELECT BorrowId, BorrowDate, ReturnDate, Users.MemberId, Users.UserName, 
Asset.AssetCode, Asset.AssetName, Asset.AssetStatus, Category.CategoryName
FROM Borrow
INNER JOIN Users ON Borrow.UserId = Users.UserId
INNER JOIN Asset ON Borrow.AssetId = Asset.AssetId
INNER JOIN Category ON Asset.CategoryId = Category.CategoryId
WHERE Borrow.IsActive = @IsActive
ORDER BY Borrow.BorrowId DESC";
                List<SqlParameter> parameters = new()
                {
                    new("@IsActive", true)
                };
                DataTable dt = DbHelper.Query(query, parameters.ToArray());
                string jsonStr = JsonConvert.SerializeObject(dt);
                List<BorrowResponseModel> lst = JsonConvert.DeserializeObject<List<BorrowResponseModel>>(jsonStr)!;

                return View(lst);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IActionResult CreateBorrow()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("name")))
            {
                TempData["error"] = "Please login first!";
                return RedirectToAction("LoginPage", "User");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Save(CreateBorrowRequestModel requestModel)
        {
            SqlConnection conn = new(_configuration.GetConnectionString("DbConnection"));
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();
            try
            {
                string userFetchQuery = @"SELECT [UserId]
  FROM [dbo].[Users] WHERE MemberId = @MemberId AND IsActive = @IsActive";
                SqlCommand userFetchCmd = new(userFetchQuery, conn);
                userFetchCmd.Transaction = transaction;
                userFetchCmd.Parameters.AddWithValue("@MemberId", requestModel.MemberId);
                userFetchCmd.Parameters.AddWithValue("@IsActive", true);
                SqlDataAdapter userFetchAdapter = new(userFetchCmd);
                DataTable userDt = new();
                userFetchAdapter.Fill(userDt);
                if (userDt.Rows.Count == 0)
                {
                    TempData["error"] = "User does not exist!";
                    return RedirectToAction("BorrowManagement");
                }
                long userID = Convert.ToInt64(userDt.Rows[0]["UserId"]);


                string assetFetchQuery = @"SELECT AssetId, Quantity
  FROM [dbo].[Asset] WHERE AssetCode = @AssetCode AND IsActive = @IsActive";
                SqlCommand assetCmd = new(assetFetchQuery, conn);
                assetCmd.Transaction = transaction;
                assetCmd.Parameters.AddWithValue("@AssetCode", requestModel.AssetCode);
                assetCmd.Parameters.AddWithValue("@IsActive", true);
                SqlDataAdapter assetAdapter = new(assetCmd);
                DataTable asset = new();
                assetAdapter.Fill(asset);
                if (asset.Rows.Count == 0)
                {
                    TempData["error"] = "Asset does not exist!";
                    return RedirectToAction("BorrowManagement");
                }
                long assetID = Convert.ToInt64(asset.Rows[0]["AssetId"]);
                int quantity = Convert.ToInt32(asset.Rows[0]["Quantity"]);

                // reduce
                string reduceQuantityQuery = @"UPDATE Asset SET Quantity = @Quantity WHERE AssetId = @AssetId";
                SqlCommand cmd = new(reduceQuantityQuery, conn)
                {
                    Transaction = transaction
                };
                cmd.Parameters.AddWithValue("@Quantity", quantity - 1);
                cmd.Parameters.AddWithValue("@AssetId", assetID);
                int result = cmd.ExecuteNonQuery();

                // insert case
                string insertQuery = @"INSERT INTO Borrow (UserId, AssetId, BorrowDate, ReturnDate, IsActive)
VALUES (@UserId, @AssetId, @BorrowDate, @ReturnDate, @IsActive)";
                SqlCommand insertCmd = new(insertQuery, conn)
                {
                    Transaction = transaction
                };
                insertCmd.Parameters.AddWithValue("@UserId", userID);
                insertCmd.Parameters.AddWithValue("@AssetId", assetID);
                insertCmd.Parameters.AddWithValue("@BorrowDate", DateTime.Now);
                insertCmd.Parameters.AddWithValue("@ReturnDate", DBNull.Value);
                insertCmd.Parameters.AddWithValue("@IsActive", true);
                int insertResult = insertCmd.ExecuteNonQuery();

                if (result > 0 && insertResult > 0)
                {
                    transaction.Commit();
                    TempData["success"] = "Creating Successful!";
                    return RedirectToAction("BorrowManagement");
                }

                TempData["error"] = "Creating Fail!";
                transaction.Rollback();
                return RedirectToAction("BorrowManagement");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public IActionResult EditBorrow(long id)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("name")))
                {
                    TempData["error"] = "Please login first!";
                    return RedirectToAction("LoginPage", "User");
                }

                string query = @"SELECT [BorrowId]
      ,[UserId]
      ,[AssetId]
      ,[BorrowDate]
      ,[ReturnDate]
      ,[IsActive]
  FROM [dbo].[Borrow] WHERE IsActive = @IsActive AND BorrowId = @BorrowId";
                List<SqlParameter> parameters = new()
                {
                    new("@IsActive", true),
                    new("@BorrowId", id)
                };

                DataTable dt = DbHelper.Query(query, parameters.ToArray());

                if (dt.Rows.Count == 0)
                {
                    TempData["error"] = "No data found.";
                    return RedirectToAction("BorrowManagement");
                }

                BorrowDataModel borrow = new()
                {
                    BorrowId = Convert.ToInt64(dt.Rows[0]["BorrowId"]),
                    ReturnDate = Convert.ToString(dt.Rows[0]["ReturnDate"])!
                };

                return View(borrow);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult UpdateBorrow(UpdateBorrowRequestModel requestModel)
        {
            try
            {
                string query = @"UPDATE Borrow SET ReturnDate = @ReturnDate WHERE BorrowId = @BorrowId";
                List<SqlParameter> parameters = new()
                {
                    new("@BorrowId", requestModel.BorrowId),
                    new("@ReturnDate", requestModel.ReturnDate)
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

                return RedirectToAction("BorrowManagement");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IActionResult DeleteBorrow(long id)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("name")))
                {
                    TempData["error"] = "Please login first!";
                    return RedirectToAction("LoginPage", "User");
                }

                string query = @"UPDATE Borrow SET IsActive = @IsActive WHERE BorrowId = @BorrowId";
                List<SqlParameter> parameters = new()
                {
                    new("@IsActive", false),
                    new("@BorrowId", id)
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
                return RedirectToAction("BorrowManagement");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
