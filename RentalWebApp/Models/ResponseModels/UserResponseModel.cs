namespace RentalWebApp.Models.ResponseModels
{
    public class UserResponseModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserRole { get; set; }
        public bool IsActive { get; set; }
    }
}
