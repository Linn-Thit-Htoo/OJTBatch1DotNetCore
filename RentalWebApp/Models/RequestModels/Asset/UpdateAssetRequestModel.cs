namespace RentalWebApp.Models.RequestModels.Asset
{
    public class UpdateAssetRequestModel
    {
        public long AssetId { get; set; }
        public long CategoryId { get; set; }
        public string AssetCode { get; set; } = null!;
        public string AssetName { get; set; } = null!;
        public string AssetStatus { get; set; }
        public int Quantity { get; set; }
    }
}