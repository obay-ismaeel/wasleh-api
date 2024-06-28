using Wasleh.Domain.Entities;

namespace Wasleh.Dtos;

public record QuestionRequestDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
    public int TotalVotes { get; set; }
    public int UserId { get; set; }
}
