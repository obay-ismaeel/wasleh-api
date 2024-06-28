namespace Wasleh.Domain.Entities;

public class Question : BaseEntity
{
    public string? Title { get; set; }
    public string? Body { get; set; }
    public int TotalVotes { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
