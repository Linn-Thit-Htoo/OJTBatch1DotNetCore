using ExpenseTrackerApi.Models.Entities;
using ExpenseTrackerApi.Models.RequestModels.ExpenseCategory;
using ExpenseTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseTrackerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseCategoryController : ControllerBase
{
    private readonly AdoDotNetService _service;

    public ExpenseCategoryController(AdoDotNetService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("/api/expense-category")]
    public IActionResult GetList()
    {
        try
        {
            string query = @"SELECT [ExpenseCategoryId]
      ,[ExpenseCategoryName]
      ,[IsActive]
  FROM [dbo].[Rest_Expense_Category] WHERE IsActive = @IsActive ORDER BY ExpenseCategoryId DESC";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@IsActive", true)
            };
            List<ExpenseCategoryModel> lst = _service.Query<ExpenseCategoryModel>(query, parameters.ToArray());

            return Ok(lst);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpPost]
    [Route("/api/expense-category")]
    public IActionResult CreateExpenseCategory([FromBody] ExpenseCategoryRequestModel requestModel)
    {
        try
        {
            if (string.IsNullOrEmpty(requestModel.ExpenseCategoryName))
                return BadRequest("Category name cannot be empty.");

            string duplicateQuery = @"SELECT [ExpenseCategoryId]
      ,[ExpenseCategoryName]
      ,[IsActive]
  FROM [dbo].[Rest_Expense_Category] WHERE ExpenseCategoryName = @ExpenseCategoryName AND IsActive = @IsActive";
            List<SqlParameter> duplicateParams = new()
            {
                new SqlParameter("@ExpenseCategoryName", requestModel.ExpenseCategoryName),
                new SqlParameter("@IsActive", true)
            };
            DataTable dt = _service.QueryFirstOrDefault(duplicateQuery, duplicateParams.ToArray());
            if (dt.Rows.Count > 0)
                return Conflict("Expense Category Name already exists!");

            string query = @"INSERT INTO Rest_Expense_Category (ExpenseCategoryName, IsActive) VALUES (@ExpenseCategoryName, @IsActive)";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@ExpenseCategoryName", requestModel.ExpenseCategoryName),
                new SqlParameter("@IsActive", true)
            };
            int result = _service.Execute(query, parameters.ToArray());

            return result > 0 ? StatusCode(201, "Creating Successful!") : BadRequest("Creating Fail!");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpPut]
    [Route("/api/expense-category/{id}")]
    public IActionResult UpdateExpenseCategory([FromBody] ExpenseCategoryRequestModel requestModel, long id)
    {
        try
        {
            string duplicateQuery = @"SELECT [ExpenseCategoryId]
      ,[ExpenseCategoryName]
      ,[IsActive]
  FROM [dbo].[Rest_Expense_Category] WHERE ExpenseCategoryName = @ExpenseCategoryName AND
IsActive = @IsActive AND
ExpenseCategoryId != @ExpenseCategoryId";
            List<SqlParameter> duplicateParams = new()
            {
                new SqlParameter("@ExpenseCategoryName", requestModel.ExpenseCategoryName),
                new SqlParameter("@IsActive", true),
                new SqlParameter("@ExpenseCategoryId", id)
            };
            DataTable dt = _service.QueryFirstOrDefault(duplicateQuery, duplicateParams.ToArray());
            if (dt.Rows.Count > 0)
                return Conflict("Expense Category Name already exists.");

            string query = @"UPDATE Rest_Expense_Category SET ExpenseCategoryName = @ExpenseCategoryName WHERE
ExpenseCategoryId = @ExpenseCategoryId";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@ExpenseCategoryName", requestModel.ExpenseCategoryName),
                new SqlParameter("@ExpenseCategoryId", id)
            };
            int result = _service.Execute(query, parameters.ToArray());

            return result > 0 ? StatusCode(202, "Updating Successful!") : BadRequest("Updating Fail!");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // delete
    [HttpDelete]
    [Route("/api/expense-category/{id}")]
    public IActionResult DeleteExpense(long id)
    {
        try
        {
            string validateQuery = @"SELECT [ExpenseId]
      ,[ExpenseCategoryId]
      ,[Amount]
      ,[IsActive]
  FROM [dbo].[Rest_Expense] WHERE ExpenseCategoryId = @ExpenseCategoryId";
            List<SqlParameter> validateParams = new()
            {
                new SqlParameter("@ExpenseCategoryId", id)
            };
            DataTable dt = _service.QueryFirstOrDefault(validateQuery, validateParams.ToArray());

            if (dt.Rows.Count > 0)
                return Conflict("Expense with this category already exists! Cannot delete.");

            string query = @"UPDATE Rest_Expense SET IsActive = @IsActive WHERE ExpenseId = @ExpenseId";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@ExpenseId", id),
                new SqlParameter("@IsActive", false)
            };
            int result = _service.Execute(query, parameters.ToArray());

            return result > 0 ? StatusCode(202, "Deleting Successful!") : BadRequest("Deleting Fail!");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
