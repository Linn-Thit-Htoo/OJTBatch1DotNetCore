namespace ExpenseTrackerApi.Models.Entities;

public class IncomeModel
{
    public long IncomeId { get; set; }
    public long UserId { get; set; }
    public long IncomeCategoryId { get; set; }
    public long Amount { get; set; }
    public bool IsActive { get; set; }
}