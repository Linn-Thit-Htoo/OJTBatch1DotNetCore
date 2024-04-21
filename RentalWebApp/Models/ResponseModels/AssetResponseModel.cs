namespace RentalWebApp.Models.ResponseModels
{
    public class AssetResponseModel
    {
        public long AssetId { get; set; }
        public string CategoryName { get; set; }
        public string AssetName { get; set; }
        public string AssetStatus { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
    }
}
