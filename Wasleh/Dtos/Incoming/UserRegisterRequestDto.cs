using System.ComponentModel.DataAnnotations;

namespace Wasleh.Dtos.Incoming;

public record UserRegisterRequestDto
{
    [EmailAddress]
    [MinLength(5)]
    public string? Email { get; set; }
    [MinLength(6)]
    public string? Password { get; set; }
    [MinLength(6)]
    public string? ConfirmPassword { get; set; }
}
