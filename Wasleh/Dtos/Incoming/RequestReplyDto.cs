using Wasleh.Domain.Entities;

namespace Wasleh.Dtos.Incoming;

public record RequestReplyDto
{
    public int Id { get; set; }
    public string? Body { get; set; }
    public int AnswerId { get; set; }
    public int UserId { get; set; }
}
