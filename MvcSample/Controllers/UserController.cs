using Microsoft.AspNetCore.Mvc;
using MvcSample.Helper;
using MvcSample.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace MvcSample.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserDataModel dataModel)
        {
            try
            {
                if (string.IsNullOrEmpty(dataModel.PhoneNumber) || string.IsNullOrEmpty(dataModel.Password))
                {
                    TempData["fail"] = "Login Fail!";
                    return RedirectToAction("Index");
                }

                SqlConnection conn = new(_configuration.GetConnectionString("DbConnection"));
                conn.Open();
                string query = @"SELECT [UserId]
      ,[UserName]
      ,[PhoneNumber]
      ,[UserRole]
      ,[IsActive]
  FROM [dbo].[Users] WHERE PhoneNumber = @PhoneNumber AND Password = @Password AND IsActive = @IsActive";
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@PhoneNumber", dataModel.PhoneNumber);
                cmd.Parameters.AddWithValue("@Password", dataModel.Password);
                cmd.Parameters.AddWithValue("@IsActive", true);
                SqlDataAdapter adapter = new(cmd);
                DataTable dt = new();
                adapter.Fill(dt);
                conn.Close();

                if (dt.Rows.Count > 0)
                {
                    TempData["successMessage"] = "Login Successful!";
                }
                else
                {
                    TempData["fail"] = "Login Fail!";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
