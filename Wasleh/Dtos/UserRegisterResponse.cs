namespace Wasleh.Dtos;

public record UserRegisterResponse
{
    public string Message { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public IEnumerable<string> Errors { get; set; } = [];
    public DateTime? ExpireDate { get; set; }
    public string? JwtToken { get; set; }
}
