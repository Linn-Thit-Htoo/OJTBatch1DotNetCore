namespace ExpenseTrackerApi.Models.RequestModels.Expense;

public class ExpenseRequestModel
{
    public long ExpenseCategoryId { get; set; }
    public long UserId { get; set; }
    public long Amount { get; set; }
}