namespace OJTBatch1DotNetCore.RestApiDemo.Models;

public class RegisterRequestModel
{
    public string UserName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
}