namespace OJTBatch1DotNetCore.RestApiDemo.Models;

public class UserModel
{
    public long UserId { get; set; }
    public string MemberId { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string UserRole { get; set; } = null!;
    public bool IsActive { get; set; }
}