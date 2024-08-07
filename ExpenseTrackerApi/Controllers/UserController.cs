﻿using System.Data;
using System.Data.SqlClient;
using ExpenseTrackerApi.Enums;
using ExpenseTrackerApi.Models.RequestModels.User;
using ExpenseTrackerApi.Models.ResponseModels.User;
using ExpenseTrackerApi.Queries;
using ExpenseTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApi.Controllers;

public class UserController : ControllerBase
{
    private readonly AdoDotNetService _adoDotNetService;
    private readonly IConfiguration _configuration;

    public UserController(AdoDotNetService service, IConfiguration configuration)
    {
        _adoDotNetService = service;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("/api/account/register")]
    public IActionResult Register([FromBody] RegisterRequestModel requestModel)
    {
        SqlConnection conn = new(_configuration.GetConnectionString("DbConnection"));
        conn.Open();
        SqlTransaction transaction = conn.BeginTransaction();
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

            string duplicateQuery = UserQuery.GetDuplicateEmailQuery();
            List<SqlParameter> duplicateParams =
                new()
                {
                    new SqlParameter("@Email", requestModel.Email),
                    new SqlParameter("@IsActive", true)
                };
            DataTable user = _adoDotNetService.QueryFirstOrDefault(
                duplicateQuery,
                duplicateParams.ToArray()
            );
            if (user.Rows.Count > 0)
            {
                return Conflict("User with this email already exists. Please login.");
            }
            string query = UserQuery.GetRegisterQuery();
            SqlCommand cmd = new(query, conn) { Transaction = transaction };
            List<SqlParameter> parameters =
                new()
                {
                    new SqlParameter("@UserName", requestModel.UserName),
                    new SqlParameter("@Email", requestModel.Email),
                    new SqlParameter("@Password", requestModel.Password),
                    new SqlParameter("@UserRole", EnumUserRoles.User.ToString()),
                    new SqlParameter("@DOB", requestModel.DOB),
                    new SqlParameter("@Gender", requestModel.Gender),
                    new SqlParameter("@IsActive", true)
                };
            cmd.Parameters.AddRange(parameters.ToArray());
            long userID = Convert.ToInt64(cmd.ExecuteScalar());

            int result = 0;
            if (userID != 0)
            {
                string balanceQuery = BalanceQuery.CreateBalanceQuery();
                List<SqlParameter> balanceParams =
                    new()
                    {
                        new SqlParameter("@UserId", userID),
                        new SqlParameter("@Amount", "0"),
                        new SqlParameter("@CreateDate", DateTime.Now)
                    };
                SqlCommand balanceCmd = new(balanceQuery, conn) { Transaction = transaction };
                balanceCmd.Parameters.AddRange(balanceParams.ToArray());
                result = balanceCmd.ExecuteNonQuery();
            }

            if (userID == 0 || result == 0 || result < 0)
            {
                transaction.Rollback();
                return BadRequest("Registration Fail.");
            }

            transaction.Commit();
            conn.Close();
            return StatusCode(201, "Registration Successful.");
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("/api/account/login")]
    public IActionResult Login([FromBody] LoginRequestModel requestModel)
    {
        try
        {
            if (
                requestModel is null
                || string.IsNullOrEmpty(requestModel.Email)
                || string.IsNullOrEmpty(requestModel.Password)
            )
                return BadRequest("Email or Password is empty.");
            string query = UserQuery.GetLoginQuery();
            List<SqlParameter> parameters =
                new()
                {
                    new SqlParameter("@Email", requestModel.Email),
                    new SqlParameter("@Password", requestModel.Password),
                    new SqlParameter("@IsActive", true),
                };
            DataTable user = _adoDotNetService.QueryFirstOrDefault(query, parameters.ToArray());
            if (user.Rows.Count == 0)
                return NotFound("User not found. Login Fail.");

            LoginResponseModel responseModel =
                new()
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
