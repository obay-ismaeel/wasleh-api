using System.ComponentModel.DataAnnotations;

namespace Wasleh.Dtos.Incoming;

public record UserLoginRequestDto
{
    [EmailAddress]
    public string? Email { get; set; }
    public string? Password { get; set; }
}
