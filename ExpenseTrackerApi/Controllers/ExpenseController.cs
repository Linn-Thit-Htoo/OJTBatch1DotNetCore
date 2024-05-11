using ExpenseTrackerApi.Models.Entities;
using ExpenseTrackerApi.Models.RequestModels.Expense;
using ExpenseTrackerApi.Models.ResponseModels.Expense;
using ExpenseTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ExpenseTrackerApi.Controllers;

public class ExpenseController : ControllerBase
{
    private readonly AdoDotNetService _service;

    public ExpenseController(AdoDotNetService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("/api/expense")]
    public IActionResult GetList()
    {
        try
        {
            string query = @"SELECT ExpenseId, Rest_Expense_Category.ExpenseCategoryName, Rest_Users.UserName, Amount, Rest_Expense.IsActive
FROM Rest_Expense
INNER JOIN Rest_Expense_Category ON Rest_Expense.ExpenseCategoryId = Rest_Expense_Category.ExpenseCategoryId
INNER JOIN Rest_Users ON Rest_Expense.UserId = Rest_Users.UserId
WHERE Rest_Expense.IsActive = @IsActive
ORDER BY ExpenseId DESC";
            SqlParameter[] sqlParameters = { new SqlParameter("@IsActive", true) };
            List<ExpenseResponseModel> lst = _service.Query<ExpenseResponseModel>(query, sqlParameters);

            return Ok(lst);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpPost]
    [Route("/api/expense")]
    public IActionResult CreateExpense([FromBody] ExpenseRequestModel requestModel)
    {
        try
        {
            if (requestModel.ExpenseCategoryId == 0 || requestModel.Amount == 0 || requestModel.UserId == 0)
                return BadRequest();

            string query = @"INSERT INTO Rest_Expense (ExpenseCategoryId, UserId, Amount, IsActive)
VALUES (@ExpenseCategoryId, @UserId, @Amount, @IsActive)";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@ExpenseCategoryId", requestModel.ExpenseCategoryId),
                new SqlParameter("@UserId", requestModel.UserId),
                new SqlParameter("@Amount", requestModel.Amount),
                new SqlParameter("@IsActive", true),
            };
            int result = _service.Execute(query, parameters.ToArray());

            return result > 0 ? StatusCode(201, "Expense Created!") : BadRequest("Creating Fail!");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpPut]
    [Route("/api/expense/{id}")]
    public IActionResult UpdateExpense([FromBody] ExpenseRequestModel requestModel, long id)
    {
        try
        {
            if (requestModel.ExpenseCategoryId == 0 || requestModel.Amount == 0 || id == 0 || requestModel.UserId == 0)
                return BadRequest();

            string query = @"UPDATE Rest_Expense SET ExpenseCategoryId = @ExpenseCategoryId, 
Amount = @Amount WHERE ExpenseId = @ExpenseId AND UserId = @UserId";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@ExpenseId", id),
                new SqlParameter("@UserId", requestModel.UserId),
                new SqlParameter("@ExpenseCategoryId", requestModel.ExpenseCategoryId),
                new SqlParameter("@Amount", requestModel.Amount),
                new SqlParameter("@IsActive", requestModel.Amount)
            };
            int result = _service.Execute(query, parameters.ToArray());

            return result > 0 ? StatusCode(201, "Expense Updated!") : BadRequest("Updating Fail!");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpDelete]
    [Route("/api/expense/{id}")]
    public IActionResult DeleteExpense(long id)
    {
        try
        {
            if (id == 0)
                return BadRequest();

            string query = @"UPDATE Rest_Expense SET IsActive = @IsActive WHERE ExpenseId = @ExpenseId";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@IsActive", false),
                new SqlParameter("@ExpenseId", id)
            };
            int result = _service.Execute(query, parameters.ToArray());

            return result > 0 ? StatusCode(201, "Income Deleted!") : BadRequest("Deleting Fail!");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}