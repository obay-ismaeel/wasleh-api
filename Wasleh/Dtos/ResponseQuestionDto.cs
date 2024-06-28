using Wasleh.Domain.Entities;

namespace Wasleh.Dtos;

internal record ResponseQuestionDto
{
    public string? Title { get; set; }
    public string? Body { get; set; }
    public int TotalVotes { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime? CreatedAt { get; set; }
}