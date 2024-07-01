namespace Wasleh.Domain.Entities;

public class Answer : BaseEntity
{
    public string? Body { get; set; }
    public int TotalVotes { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public Question Question { get; set; }
    public int QuestionId { get; set; }
    public ICollection<Reply> Replies { get; set; } = [];
}
