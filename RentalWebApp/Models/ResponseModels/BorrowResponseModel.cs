namespace RentalWebApp.Models.ResponseModels
{
    public class BorrowResponseModel
    {
        public long BorrowId { get; set; }
        public DateTime BorrowDate { get; set; }
        public string ReturnDate { get; set; }
        public string MemberId { get; set; }
        public string UserName { get; set; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public string AssetStatus { get; set; }
        public string CategoryName { get; set; }
    }
}
