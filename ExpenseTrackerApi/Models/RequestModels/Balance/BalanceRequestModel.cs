namespace ExpenseTrackerApi.Models.RequestModels.Balance;

public class BalanceRequestModel
{
    public long UserId { get; set; }
    public string Amount { get; set; } = null!;
}
