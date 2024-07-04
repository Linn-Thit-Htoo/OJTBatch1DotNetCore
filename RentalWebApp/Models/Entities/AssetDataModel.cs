namespace RentalWebApp.Models.Entities;

public class AssetDataModel
{
    public long AssetId { get; set; }
    public long CategoryId { get; set; }
    public string AssetCode { get; set; }
    public string AssetName { get; set; }
    public string AssetStatus { get; set; }
    public int Quantity { get; set; }
    public DateTime CreateDate { get; set; }
    public bool IsActive { get; set; }
}
