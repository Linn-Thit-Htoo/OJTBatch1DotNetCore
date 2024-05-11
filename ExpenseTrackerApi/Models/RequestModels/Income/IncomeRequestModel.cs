namespace ExpenseTrackerApi.Models.RequestModels.Income;

public class IncomeRequestModel
{
    public long IncomeCategoryId { get; set; }
    public long UserId { get; set; }
    public long Amount { get; set; }
}
