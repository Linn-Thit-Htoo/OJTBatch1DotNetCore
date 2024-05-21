namespace ExpenseTrackerApi.Models.ResponseModels.Expense;

public class ExpenseResponseModel
{
    public long ExpenseId { get; set; }
    public string ExpenseCategoryName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public long Amount { get; set; }
    public DateTime CreateDate { get; set; }
    public bool IsActive { get; set; }
}