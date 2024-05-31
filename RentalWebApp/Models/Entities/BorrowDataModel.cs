namespace RentalWebApp.Models.Entities;

public class BorrowDataModel
{
    public long BorrowId { get; set; }
    public long UserId { get; set; }
    public long AssetId { get; set; }
    public DateTime BorrowDate { get; set; }
    public string ReturnDate { get; set; }
    public bool IsActive { get; set; }
}