using ExpenseTrackerApi.Models.Entities;
using ExpenseTrackerApi.Models.RequestModels.Income;
using ExpenseTrackerApi.Models.ResponseModels.Income;
using ExpenseTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ExpenseTrackerApi.Controllers;

public class IncomeController : ControllerBase
{
    private readonly AdoDotNetService _adoDotNetService;

    public IncomeController(AdoDotNetService adoDotNetService)
    {
        _adoDotNetService = adoDotNetService;
    }

    [HttpGet]
    [Route("/api/income")]
    public IActionResult GetList()
    {
        try
        {
            string query = @"SELECT Rest_Income.IncomeId, Rest_Users.UserName, Rest_Income_Category.IncomeCategoryName,
Rest_Income.Amount, Rest_Income.IsActive
FROM Rest_Income
INNER JOIN Rest_Users ON Rest_Income.UserId = Rest_Users.UserId
INNER JOIN Rest_Income_Category ON Rest_Income.IncomeCategoryId = Rest_Income_Category.IncomeCategoryId
WHERE Rest_Income.IsActive = @IsActive
ORDER BY IncomeId DESC";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@IsActive", true)
            };
            List<IncomeResponseModel> lst = _adoDotNetService.Query<IncomeResponseModel>(query, parameters.ToArray());

            return Ok(lst);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpPost]
    [Route("/api/income")]
    public IActionResult CreateIncome([FromBody] IncomeRequestModel requestModel)
    {
        try
        {
            if (requestModel.IncomeCategoryId == 0 || requestModel.Amount == 0 || requestModel.UserId == 0)
                return BadRequest();

            string query = @"INSERT INTO Rest_Income (IncomeCategoryId, UserId, Amount, IsActive)
VALUES (@IncomeCategoryId, @UserId, @Amount, @IsActive)";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@IncomeCategoryId", requestModel.IncomeCategoryId),
                new SqlParameter("@UserId", requestModel.UserId),
                new SqlParameter("@Amount", requestModel.Amount),
                new SqlParameter("@IsActive", true)
            };
            int result = _adoDotNetService.Execute(query, parameters.ToArray());

            return result > 0 ? StatusCode(201, "Income Created!") : BadRequest("Creating Fail!");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpPut]
    [Route("/api/income/{id}")]
    public IActionResult UpdateIncome([FromBody] IncomeRequestModel requestModel, long id)
    {
        try
        {
            if (requestModel.IncomeCategoryId == 0 || requestModel.Amount == 0 || id == 0 || requestModel.UserId == 0)
                return BadRequest();

            string query = @"UPDATE Rest_Income SET IncomeCategoryId = @IncomeCategoryId,
Amount = @Amount WHERE IncomeId = @IncomeId AND UserId = @UserId";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@IncomeId", id),
                new SqlParameter("@UserId", requestModel.UserId),
                new SqlParameter("@IncomeCategoryId", requestModel.IncomeCategoryId),
                new SqlParameter("@Amount", requestModel.Amount)
            };
            int result = _adoDotNetService.Execute(query, parameters.ToArray());

            return result > 0 ? StatusCode(201, "Income Updated!") : BadRequest("Updating Fail!");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpDelete]
    [Route("/api/income/{id}")]
    public IActionResult DeleteIncome(long id)
    {
        try
        {
            string query = @"UPDATE Rest_Income SET IsActive = @IsActive WHERE IncomeId = @IncomeId";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@IsActive", false),
                new SqlParameter("@IncomeId", id)
            };
            int result = _adoDotNetService.Execute(query, parameters.ToArray());

            return result > 0 ? StatusCode(201, "Income Deleted!") : BadRequest("Deleting Fail!");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
