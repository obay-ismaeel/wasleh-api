using Wasleh.Domain.Entities;

namespace Wasleh.Dtos.Outgoing;

public record ResponseAnswerDto
{
    public int Id { get; set; }
    public string? Body { get; set; }
    public int TotalVotes { get; set; }
    public int UserId { get; set; }
    public int QuestionId { get; set; }
    public DateTime CreatedAt { get; set; }
}
