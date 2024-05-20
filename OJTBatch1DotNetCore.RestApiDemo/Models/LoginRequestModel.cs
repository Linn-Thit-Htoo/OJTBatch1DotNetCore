namespace OJTBatch1DotNetCore.RestApiDemo.Models;

public class LoginRequestModel
{
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
}