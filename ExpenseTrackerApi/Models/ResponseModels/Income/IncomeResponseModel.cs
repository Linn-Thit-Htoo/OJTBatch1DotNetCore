﻿namespace ExpenseTrackerApi.Models.ResponseModels.Income;

public class IncomeResponseModel
{
    public long IncomeId { get; set; }
    public string UserName { get; set; } = null!;
    public string IncomeCategoryName { get; set; } = null!;
    public long Amount { get; set; }
    public DateTime CreateDate { get; set; }
    public bool IsActive { get; set; }
}
