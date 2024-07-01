using Wasleh.Domain.Entities;

namespace Wasleh.Dtos.Outgoing;

public record ResponseReplyDto
{
    public int Id { get; set; }
    public string? Body { get; set; }
    public int AnswerId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}
