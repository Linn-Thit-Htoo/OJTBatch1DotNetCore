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
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [ActionName("LoginPage")]
        public IActionResult GoToLoginPage()
        {
            return View();
        }

        public IActionResult UserManagement()
        {
            try
            {
                SqlConnection conn = new(_configuration.GetConnectionString("DbConnection"));
                conn.Open();
                string query = @"SELECT [UserId]
      ,[MemberId]
      ,[UserName]
      ,[PhoneNumber]
      ,[UserRole]
      ,[IsActive]
  FROM [dbo].[Users] WHERE IsActive = @IsActive";
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@IsActive", true);
                SqlDataAdapter adapter = new(cmd);
                DataTable dt = new();
                adapter.Fill(dt);
                conn.Close();

                string jsonStr = JsonConvert.SerializeObject(dt); // convert to json
                List<UserResponseModel> lst = JsonConvert.DeserializeObject<List<UserResponseModel>>(jsonStr)!;

                return View(lst);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [ActionName("CreateUser")]
        public IActionResult GoToCreateUserPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserDataModel dataModel)
        {
            try
            {
                if (string.IsNullOrEmpty(dataModel.MemberId))
                    goto Fail;
                else if (string.IsNullOrEmpty(dataModel.UserName))
                    goto Fail;
                else if (string.IsNullOrEmpty(dataModel.PhoneNumber))
                    goto Fail;
                else if (dataModel.MemberId.Length > 6)
                {
                    TempData["error"] = "Member ID must be 6 letters long";
                    return RedirectToAction("UserManagement");
                }

                string duplicateTestingQuery = @"SELECT [UserId]
      ,[MemberId]
      ,[UserName]
      ,[PhoneNumber]
      ,[UserRole]
      ,[IsActive]
  FROM [dbo].[Users] WHERE IsActive = @IsActive AND MemberId = @MemberId";
                List<SqlParameter> sqlParameters = new()
                {
                     new("@IsActive", true),
                     new("@MemberId", dataModel.MemberId)
                };
                DataTable user = DbHelper.Query(duplicateTestingQuery, sqlParameters.ToArray());

                if (user.Rows.Count > 0)
                {
                    TempData["error"] = "Memeber ID already exists";
                    return RedirectToAction("UserManagement");
                }

                if (IsPhoneNumberDuplicate(dataModel.PhoneNumber))
                {
                    TempData["error"] = "User with this phone number already exists!";
                    return RedirectToAction("UserManagement");
                }

                SqlConnection conn = new(_configuration.GetConnectionString("DbConnection"));
                conn.Open();
                string query = @"INSERT INTO Users (MemberId, UserName, PhoneNumber, UserRole, IsActive)
VALUES(@MemberId, @UserName, @PhoneNumber, @UserRole, @IsActive)";
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@MemberId", dataModel.MemberId);
                cmd.Parameters.AddWithValue("@UserName", dataModel.UserName);
                cmd.Parameters.AddWithValue("@PhoneNumber", dataModel.PhoneNumber);
                cmd.Parameters.AddWithValue("@UserRole", "user");
                cmd.Parameters.AddWithValue("@IsActive", true);
                int result = cmd.ExecuteNonQuery();
                conn.Close();

                if (result > 0)
                {
                    TempData["success"] = "Creating Successful!";
                    return RedirectToAction("UserManagement");
                }

            Fail:
                TempData["error"] = "Please fill all fields...";
                return RedirectToAction("UserManagement");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IActionResult EditUser(long id)
        {
            try
            {
                SqlConnection conn = new(_configuration.GetConnectionString("DbConnection"));
                conn.Open();
                string query = @"SELECT [UserId]
      ,[UserName]
      ,[MemberId]
      ,[PhoneNumber]
      ,[UserRole]
      ,[IsActive]
  FROM [dbo].[Users] WHERE UserId = @UserId AND IsActive = @IsActive";
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@UserId", id);
                cmd.Parameters.AddWithValue("@IsActive", true);
                SqlDataAdapter adapter = new(cmd);
                DataTable dt = new();
                adapter.Fill(dt);
                conn.Close();

                string jsonStr = JsonConvert.SerializeObject(dt);
                List<UserDataModel> user = JsonConvert.DeserializeObject<List<UserDataModel>>(jsonStr)!;

                return View(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Update(UpdateUserRequestModel requestModel)
        {
            try
            {
                if (IsPhoneNumberDuplicate(requestModel.PhoneNumber, requestModel.UserId))
                {
                    TempData["error"] = "User with this phone already exists!";
                    return RedirectToAction("UserManagement");
                }

                string duplicateTestingQuery = @"SELECT [UserId]
      ,[MemberId]
      ,[UserName]
      ,[PhoneNumber]
      ,[UserRole]
      ,[IsActive]
  FROM [dbo].[Users] WHERE IsActive = @IsActive AND MemberId = @MemberId AND UserId != @UserId";
                List<SqlParameter> sqlParameters = new()
                {
                     new("@IsActive", true),
                     new("@MemberId", requestModel.MemberId),
                     new("@UserId", requestModel.UserId)
                };
                DataTable user = DbHelper.Query(duplicateTestingQuery, sqlParameters.ToArray());

                if (user.Rows.Count > 0)
                {
                    TempData["error"] = "Memeber ID already exists";
                    return RedirectToAction("UserManagement");
                }

                SqlConnection conn = new(_configuration.GetConnectionString("DbConnection"));
                conn.Open();
                string query = @"UPDATE Users SET UserName = @UserName, PhoneNumber = @PhoneNumber
WHERE UserId = @UserId AND IsActive = @IsActive";
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@UserId", requestModel.UserId);
                cmd.Parameters.AddWithValue("@UserName", requestModel.UserName);
                cmd.Parameters.AddWithValue("@PhoneNumber", requestModel.PhoneNumber);
                cmd.Parameters.AddWithValue("@IsActive", true);
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

                return RedirectToAction("UserManagement");
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
                string query = @"UPDATE Users SET IsActive = @IsActive WHERE UserId = @UserId";
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@UserId", id);
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
                return RedirectToAction("UserManagement");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool IsPhoneNumberDuplicate(string phoneNumber, long? id = 0)
        {
            try
            {
                SqlConnection conn = new(_configuration.GetConnectionString("DbConnection"));
                conn.Open();
                string query = "";

                // create case
                if (id == 0)
                {
                    query = @"SELECT [UserId]
      ,[UserName]
      ,[PhoneNumber]
      ,[UserRole]
      ,[IsActive]
  FROM [dbo].[Users] WHERE PhoneNumber = @PhoneNumber AND IsActive = @IsActive";
                }
                else
                {
                    query = @"SELECT [UserId]
      ,[UserName]
      ,[PhoneNumber]
      ,[UserRole]
      ,[IsActive]
  FROM [dbo].[Users] WHERE UserId != @UserId AND PhoneNumber = @PhoneNumber AND IsActive = @IsActive";
                }
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                cmd.Parameters.AddWithValue("@IsActive", true);
                if (id != 0)
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                }
                SqlDataAdapter adapter = new(cmd);
                DataTable dt = new();
                adapter.Fill(dt);
                conn.Close();

                return dt.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
