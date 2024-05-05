using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi.Models.RequestModels.User;

public class LoginRequestModel
{
    [EmailAddress]
    public required string Email { get; set; }
    public required string Password { get; set; }
}