namespace RentalWebApp.Models.RequestModels.Borrow
{
    public class CreateBorrowRequestModel
    {
        public string MemberId { get; set; }
        public string AssetCode { get; set; }
        public string ReturnDate { get; set; }
    }
}
