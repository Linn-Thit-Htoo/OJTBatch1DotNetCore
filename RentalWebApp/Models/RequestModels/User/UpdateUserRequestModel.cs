namespace RentalWebApp.Models.RequestModels.User
{
    public class UpdateUserRequestModel
    {
        public long UserId { get; set; }
        public string? MemberId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
