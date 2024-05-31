namespace MvcSample.Models;

public class UserDataModel
{
    public long UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string PhoneNumber { get; set; }
    public string? Password { get; set; }
    public string UserRole { get; set; }
    public bool IsActive { get; set; }
}