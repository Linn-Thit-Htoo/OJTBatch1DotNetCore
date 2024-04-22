namespace RentalWebApp.Models.RequestModels
{
    public class UpdateAssetRequestModel
    {
        public long AssetId { get; set; }
        public long CategoryId { get; set; }
        public string AssetName { get; set; }
        public string AssetStatus { get; set; }
    }
}
