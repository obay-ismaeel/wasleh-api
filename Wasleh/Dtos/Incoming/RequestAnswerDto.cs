using Wasleh.Domain.Entities;

namespace Wasleh.Dtos.Incoming;

public record RequestAnswerDto
{
    public int Id { get; set; }
    public string? Body { get; set; }
    public int UserId { get; set; }
    public int QuestionId { get; set; }
}
