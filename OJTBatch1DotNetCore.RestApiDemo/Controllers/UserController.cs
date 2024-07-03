using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OJTBatch1DotNetCore.RestApiDemo.Models;
using OJTBatch1DotNetCore.RestApiDemo.Services;

namespace OJTBatch1DotNetCore.RestApiDemo.Controllers;

public class UserController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly AdoDotNetService _adoDotNetService;

    public UserController(IConfiguration configuration, AdoDotNetService adoDotNetService)
    {
        _configuration = configuration;
        _adoDotNetService = adoDotNetService;
    }

    [HttpPost]
    [Route("/api/account/register")]
    public IActionResult Register([FromBody] RegisterRequestModel requestModel)
    {
        try
        {
            if (string.IsNullOrEmpty(requestModel.UserName))
                return BadRequest("User Name cannot be empty.");

            if (string.IsNullOrEmpty(requestModel.PhoneNumber))
                return BadRequest("PhoneNumber cannot be empty.");

            if (string.IsNullOrEmpty(requestModel.Password))
                return BadRequest("Password cannot be empty.");

            string duplicateQuery =
                @"SELECT [UserId]
      ,[MemberId]
      ,[UserName]
      ,[PhoneNumber]
      ,[UserRole]
      ,[IsActive]
  FROM [dbo].[Users] WHERE PhoneNumber = @PhoneNumber AND IsActive = @IsActive";
            List<SqlParameter> duplicateParams =
                new()
                {
                    new SqlParameter("@PhoneNumber", requestModel.PhoneNumber),
                    new SqlParameter("@IsActive", true)
                };
            DataTable dt = _adoDotNetService.QueryFirstOrDefault(
                duplicateQuery,
                duplicateParams.ToArray()
            );

            if (dt.Rows.Count > 0)
            {
                return Conflict("User with this phone number already exists!");
            }

            string query =
                @"INSERT INTO Users (UserName, PhoneNumber, Password, UserRole, IsActive)
VALUES (@UserName, @PhoneNumber, @Password, @UserRole, @IsActive)";
            List<SqlParameter> parameters =
                new()
                {
                    new SqlParameter("@UserName", requestModel.UserName),
                    new SqlParameter("@PhoneNumber", requestModel.PhoneNumber),
                    new SqlParameter("@Password", requestModel.Password),
                    new SqlParameter("@UserRole", "member"),
                    new SqlParameter("@IsActive", true)
                };
            int result = _adoDotNetService.Execute(query, parameters.ToArray());

            return result > 0 ? StatusCode(201, "Registration Successful!") : BadRequest("Fail!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("/api/account/login")]
    public IActionResult Login([FromBody] LoginRequestModel requestModel)
    {
        try
        {
            if (string.IsNullOrEmpty(requestModel.PhoneNumber))
                return BadRequest("PhoneNumber cannot be empty.");

            if (string.IsNullOrEmpty(requestModel.Password))
                return BadRequest("Password cannot be empty.");

            string query =
                @"SELECT [UserId]
      ,[MemberId]
      ,[UserName]
      ,[PhoneNumber]
      ,[UserRole]
      ,[IsActive]
  FROM [dbo].[Users] WHERE PhoneNumber = @PhoneNumber AND IsActive = @IsActive AND Password = @Password";
            List<SqlParameter> parameters =
                new()
                {
                    new SqlParameter("@PhoneNumber", requestModel.PhoneNumber),
                    new SqlParameter("@Password", requestModel.Password),
                    new SqlParameter("@IsActive", true),
                };
            DataTable user = _adoDotNetService.QueryFirstOrDefault(query, parameters.ToArray());

            if (user.Rows.Count == 0)
                return NotFound("User Not found.");

            string jsonStr = JsonConvert.SerializeObject(user);
            List<UserModel> lst = JsonConvert.DeserializeObject<List<UserModel>>(jsonStr)!;

            return Ok(lst[0]);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
