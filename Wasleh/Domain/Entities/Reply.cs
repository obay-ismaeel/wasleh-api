namespace Wasleh.Domain.Entities;

public class Reply : BaseEntity
{
    public string Body { get; set; }
    public int AnswerId { get; set; }
    public Answer Answer { get; set; }
    public int  UserId { get; set; }
    public User User { get; set; }
}
