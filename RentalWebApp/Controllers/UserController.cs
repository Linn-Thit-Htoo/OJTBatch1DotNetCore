using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentalWebApp.Models.Entities;
using RentalWebApp.Models.ResponseModels;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

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
                if (string.IsNullOrEmpty(dataModel.UserName) || string.IsNullOrEmpty(dataModel.PhoneNumber))
                {
                    TempData["error"] = "Please fill all fields...";
                    return RedirectToAction("UserManagement");
                }

                if (IsPhoneNumberDuplicate(dataModel.PhoneNumber))
                {
                    TempData["error"] = "User with this phone number already exists!";
                    return RedirectToAction("UserManagement");
                }

                SqlConnection conn = new(_configuration.GetConnectionString("DbConnection"));
                conn.Open();
                string query = @"INSERT INTO Users (UserName, PhoneNumber, UserRole, IsActive)
VALUES(@UserName, @PhoneNumber, @UserRole, @IsActive)";
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@UserName", dataModel.UserName);
                cmd.Parameters.AddWithValue("@PhoneNumber", dataModel.PhoneNumber);
                cmd.Parameters.AddWithValue("@UserRole", "user");
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

        private bool IsPhoneNumberDuplicate(string phoneNumber)
        {
            try
            {
                SqlConnection conn = new(_configuration.GetConnectionString("DbConnection"));
                conn.Open();
                string query = @"SELECT [UserId]
      ,[UserName]
      ,[PhoneNumber]
      ,[UserRole]
      ,[IsActive]
  FROM [dbo].[Users] WHERE PhoneNumber = @PhoneNumber AND IsActive = @IsActive";
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                cmd.Parameters.AddWithValue("@IsActive", true);
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
