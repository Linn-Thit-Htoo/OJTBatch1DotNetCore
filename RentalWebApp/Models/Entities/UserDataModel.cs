namespace RentalWebApp.Models.Entities;

public class UserDataModel
{
    public long UserId { get; set; }
    public string? MemberId { get; set; }
    public string UserName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string UserRole { get; set; } = null!;
    public bool IsActive { get; set; }
}