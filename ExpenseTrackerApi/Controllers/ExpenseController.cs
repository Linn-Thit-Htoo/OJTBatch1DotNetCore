﻿using System.Data.SqlClient;
using ExpenseTrackerApi.Models.RequestModels.Expense;
using ExpenseTrackerApi.Models.ResponseModels.Expense;
using ExpenseTrackerApi.Queries;
using ExpenseTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

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
            string query = ExpenseQuery.GetExpenseListQuery();
            SqlParameter[] sqlParameters = { new SqlParameter("@IsActive", true) };
            List<ExpenseResponseModel> lst = _service.Query<ExpenseResponseModel>(
                query,
                sqlParameters
            );

            return Ok(lst);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpGet]
    [Route("/api/expense/{userID}")]
    public IActionResult GetExpenseListByUserId(long userID)
    {
        try
        {
            if (userID <= 0)
                return BadRequest("User Id cannot be empty.");

            string query = ExpenseQuery.GetExpenseListByUserIdQuery();
            List<SqlParameter> parameters =
                new() { new SqlParameter("@UserId", userID), new SqlParameter("@IsActive", true) };
            List<ExpenseResponseModel> lst = _service.Query<ExpenseResponseModel>(
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
    [Route("/api/expense")]
    public IActionResult CreateExpense([FromBody] ExpenseRequestModel requestModel)
    {
        try
        {
            if (
                requestModel.ExpenseCategoryId == 0
                || requestModel.Amount == 0
                || requestModel.UserId == 0
            )
                return BadRequest();

            string query = ExpenseQuery.CreateExpenseQuery();
            List<SqlParameter> parameters =
                new()
                {
                    new SqlParameter("@ExpenseCategoryId", requestModel.ExpenseCategoryId),
                    new SqlParameter("@UserId", requestModel.UserId),
                    new SqlParameter("@Amount", requestModel.Amount),
                    new SqlParameter("@CreateDate", DateTime.Now),
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
            if (
                requestModel.ExpenseCategoryId == 0
                || requestModel.Amount == 0
                || id == 0
                || requestModel.UserId == 0
            )
                return BadRequest();

            string query = ExpenseQuery.UpdateExpenseQuery();
            List<SqlParameter> parameters =
                new()
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

            string query = ExpenseQuery.DeleteExpenseQuery();
            List<SqlParameter> parameters =
                new() { new SqlParameter("@IsActive", false), new SqlParameter("@ExpenseId", id) };
            int result = _service.Execute(query, parameters.ToArray());

            return result > 0 ? StatusCode(201, "Income Deleted!") : BadRequest("Deleting Fail!");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
