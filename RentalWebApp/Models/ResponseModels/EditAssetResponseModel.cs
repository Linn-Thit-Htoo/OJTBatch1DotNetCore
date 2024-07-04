using RentalWebApp.Models.Entities;

namespace RentalWebApp.Models.ResponseModels;

public class EditAssetResponseModel
{
    public List<CategoryDataModel> Categories { get; set; }
    public AssetDataModel AssetDataModel { get; set; }
}
