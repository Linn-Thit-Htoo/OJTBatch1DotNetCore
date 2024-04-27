namespace RentalWebApp.Models.Entities
{
    public class UserDataModel
    {
        public long UserId { get; set; }
        public string? MemberId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
        public bool IsActive { get; set; }
    }
}
