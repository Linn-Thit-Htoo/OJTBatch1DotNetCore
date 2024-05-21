namespace ExpenseTrackerApi.Models.Entities;

public class ExpenseModel
{
    public long ExpenseId { get; set; }
    public long ExpenseCategoryId { get; set; }
    public long UserId { get; set; }
    public long Amount { get; set; }
    public DateTime CreateDate { get; set; }
    public bool IsActive { get; set; }
}