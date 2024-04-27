namespace RentalWebApp.Models.RequestModels
{
    public class CreateBorrowRequestModel
    {
        public string MemberId { get; set; }
        public string AssetCode { get; set; }
        public string ReturnDate { get; set; }
    }
}
