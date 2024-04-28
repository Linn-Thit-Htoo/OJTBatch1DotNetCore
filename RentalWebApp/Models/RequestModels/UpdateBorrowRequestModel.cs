namespace RentalWebApp.Models.RequestModels
{
    public class UpdateBorrowRequestModel
    {
        public long BorrowId { get; set; }
        public string ReturnDate { get; set; }
    }
}
