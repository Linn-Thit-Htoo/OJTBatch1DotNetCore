using ExpenseTrackerApi.Enums;
using ExpenseTrackerApi.Models.RequestModels.User;
using ExpenseTrackerApi.Models.ResponseModels.User;
using ExpenseTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseTrackerApi.Controllers;

public class UserController : ControllerBase
{
    private readonly AdoDotNetService _adoDotNetService;

    public UserController(AdoDotNetService service)
    {
        _adoDotNetService = service;
    }

    [HttpPost]
    [Route("/api/account/register")]
    public IActionResult Register([FromBody] RegisterRequestModel requestModel)
    {
        try
        {
            if (string.IsNullOrEmpty(requestModel.UserName))
                return BadRequest("User Name cannot be empty.");

            if (string.IsNullOrEmpty(requestModel.Email))
                return BadRequest("Email cannot be empty.");

            if (string.IsNullOrEmpty(requestModel.Password))
                return BadRequest("Password cannot be empty.");

            if (string.IsNullOrEmpty(requestModel.DOB))
                return BadRequest("DOB cannot be empty.");

            if (string.IsNullOrEmpty(requestModel.Gender))
                return BadRequest("Gender cannot be empty.");

            if (requestModel.Gender == EnumGender.Male.ToString())
                requestModel.Gender = EnumGender.Male.ToString();

            else if (requestModel.Gender == EnumGender.Female.ToString())
                requestModel.Gender = EnumGender.Female.ToString();

            else if (requestModel.Gender == EnumGender.Other.ToString())
                requestModel.Gender = EnumGender.Other.ToString();

            else
                return BadRequest("Invalid Gender");

            string duplicateQuery = @"SELECT [UserId]
      ,[UserName]
      ,[Email]
      ,[UserRole]
      ,[DOB]
      ,[Gender]
      ,[IsActive]
  FROM [dbo].[Rest_Users] WHERE Email = @Email AND IsActive = @IsActive";
            List<SqlParameter> duplicateParams = new()
            {
                new SqlParameter("@Email", requestModel.Email),
                new SqlParameter("@IsActive", true)
            };
            DataTable user = _adoDotNetService.QueryFirstOrDefault(duplicateQuery, duplicateParams.ToArray());
            if (user.Rows.Count > 0)
            {
                return Conflict("User with this email already exists. Please login.");
            }

            string query = @"INSERT INTO Rest_Users (UserName, Email, Password, UserRole, DOB, Gender, IsActive)
VALUES (@UserName, @Email, @Password, @UserRole, @DOB, @Gender, @IsActive)";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@UserName", requestModel.UserName),
                new SqlParameter("@Email", requestModel.Email),
                new SqlParameter("@Password", requestModel.Password),
                new SqlParameter("@UserRole", EnumUserRoles.User.ToString()),
                new SqlParameter("@DOB", requestModel.DOB),
                new SqlParameter("@Gender", requestModel.Gender),
                new SqlParameter("@IsActive", true)
            };
            int result = _adoDotNetService.Execute(query, parameters.ToArray());

            return result > 0 ? StatusCode(201, "Registration Successful.") : BadRequest("Registration Fail.");
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
            if (requestModel is null || string.IsNullOrEmpty(requestModel.Email) || string.IsNullOrEmpty(requestModel.Password))
                return BadRequest("Email or Password is empty.");

            string query = @"SELECT [UserId]
      ,[UserName]
      ,[Email]
      ,[UserRole]
      ,[DOB]
      ,[Gender]
      ,[IsActive]
  FROM [dbo].[Rest_Users] WHERE Email = @Email AND Password = @Password AND IsActive = @IsActive";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@Email", requestModel.Email),
                new SqlParameter("@Password", requestModel.Password),
                new SqlParameter("@IsActive", true),
            };
            DataTable user = _adoDotNetService.QueryFirstOrDefault(query, parameters.ToArray());
            if (user.Rows.Count == 0)
                return NotFound("User not found. Login Fail.");

            LoginResponseModel responseModel = new()
            {
                UserId = Convert.ToInt64(user.Rows[0]["UserId"]),
                UserName = Convert.ToString(user.Rows[0]["UserName"])!,
                Email = Convert.ToString(user.Rows[0]["Email"])!,
                Gender = Convert.ToString(user.Rows[0]["Gender"])!,
                DOB = Convert.ToString(user.Rows[0]["DOB"])!,
            };

            return StatusCode(202, responseModel);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}