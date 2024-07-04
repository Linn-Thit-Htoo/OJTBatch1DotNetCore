namespace RentalWebApp.Models.RequestModels.Borrow;

public class UpdateBorrowRequestModel
{
    public long BorrowId { get; set; }
    public string ReturnDate { get; set; }
}
