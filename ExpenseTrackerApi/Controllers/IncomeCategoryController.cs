using ExpenseTrackerApi.Enums;
using ExpenseTrackerApi.Models.Entities;
using ExpenseTrackerApi.Models.RequestModels.IncomeCategory;
using ExpenseTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseTrackerApi.Controllers;

public class IncomeCategoryController : ControllerBase
{
    private readonly AdoDotNetService _service;

    public IncomeCategoryController(AdoDotNetService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("/api/income-category")]
    public IActionResult GetIncomeCategory()
    {
        try
        {
            string query = @"SELECT [IncomeCategoryId]
      ,[IncomeCategoryName]
      ,[IsActive]
  FROM [dbo].[Rest_Income_Category] WHERE IsActive = @IsActive ORDER BY IncomeCategoryId DESC";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@IsActive", true)
            };
            List<IncomeCategoryModel> lst = _service.Query<IncomeCategoryModel>(query, parameters.ToArray());

            return Ok(lst);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpPost]
    [Route("/api/income-category")]
    public IActionResult CreateIncomeCategory([FromBody] IncomeCategoryRequestModel requestModel)
    {
        try
        {
            if (string.IsNullOrEmpty(requestModel.IncomeCategoryName))
                return BadRequest("Category Name cannot be empty.");

            string duplicateQuery = @"SELECT [IncomeCategoryId]
      ,[IncomeCategoryName]
      ,[IsActive]
  FROM [dbo].[Rest_Income_Category] WHERE IncomeCategoryName = @IncomeCategoryName AND IsActive = @IsActive";
            List<SqlParameter> duplicateParams = new()
            {
                new SqlParameter("@IncomeCategoryName", requestModel.IncomeCategoryName),
                new SqlParameter("@IsActive", true)
            };
            DataTable category = _service.QueryFirstOrDefault(duplicateQuery, duplicateParams.ToArray());
            if (category.Rows.Count > 0)
                return Conflict("Income Category Name already exists.");

            string query = @"INSERT INTO Rest_Income_Category (IncomeCategoryName, IsActive)
VALUES (@IncomeCategoryName, @IsActive)";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@IncomeCategoryName", requestModel.IncomeCategoryName),
                new SqlParameter("@IsActive", true)
            };
            int result = _service.Execute(query, parameters.ToArray());

            return result > 0 ? StatusCode(201, "Income Category Created.") : BadRequest("Creating Fail.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
