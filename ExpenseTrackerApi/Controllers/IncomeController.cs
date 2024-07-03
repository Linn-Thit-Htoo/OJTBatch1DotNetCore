using System.Data.SqlClient;
using ExpenseTrackerApi.Models.RequestModels.Income;
using ExpenseTrackerApi.Models.ResponseModels.Income;
using ExpenseTrackerApi.Queries;
using ExpenseTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

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
            string query = IncomeQuery.GetIncomeListQuery();
            List<SqlParameter> parameters = new() { new SqlParameter("@IsActive", true) };
            List<IncomeResponseModel> lst = _adoDotNetService.Query<IncomeResponseModel>(
                query,
                parameters.ToArray()
            );

            return Ok(lst);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpGet]
    [Route("/api/income/{userID}")]
    public IActionResult GetIncomeListByUserId(long userID)
    {
        try
        {
            if (userID <= 0)
                return BadRequest("User Id cannot be empty.");

            string query = IncomeQuery.GetIncomeListByUserIdQuery();
            List<SqlParameter> parameters =
                new() { new SqlParameter("@UserId", userID), new SqlParameter("@IsActive", true) };
            List<IncomeResponseModel> lst = _adoDotNetService.Query<IncomeResponseModel>(
                query,
                parameters.ToArray()
            );

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
            if (
                requestModel.IncomeCategoryId == 0
                || requestModel.Amount == 0
                || requestModel.UserId == 0
            )
                return BadRequest();

            string query = IncomeQuery.CreateIncomeQuery();
            List<SqlParameter> parameters =
                new()
                {
                    new SqlParameter("@IncomeCategoryId", requestModel.IncomeCategoryId),
                    new SqlParameter("@UserId", requestModel.UserId),
                    new SqlParameter("@Amount", requestModel.Amount),
                    new SqlParameter("@CreateDate", DateTime.Now),
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
            if (
                requestModel.IncomeCategoryId == 0
                || requestModel.Amount == 0
                || id == 0
                || requestModel.UserId == 0
            )
                return BadRequest();

            string query = IncomeQuery.UpdateIncomeQuery();
            List<SqlParameter> parameters =
                new()
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
            string query = IncomeQuery.DeleteIncomeQuery();
            List<SqlParameter> parameters =
                new() { new SqlParameter("@IsActive", false), new SqlParameter("@IncomeId", id) };
            int result = _adoDotNetService.Execute(query, parameters.ToArray());

            return result > 0 ? StatusCode(201, "Income Deleted!") : BadRequest("Deleting Fail!");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
