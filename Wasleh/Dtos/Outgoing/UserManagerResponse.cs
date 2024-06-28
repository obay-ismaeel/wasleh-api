using Wasleh.Domain.Entities;

namespace Wasleh.Dtos.Outgoing;

public record UserManagerResponse
{
    public User User { get; set; }
    public string? Message { get; set; }
    public bool IsSuccess { get; set; }
    public IEnumerable<string> Errors { get; set; } = [];
    public DateTime? ExpiryDate { get; set; }
}