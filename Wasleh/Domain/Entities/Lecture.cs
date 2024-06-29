namespace Wasleh.Domain.Entities;

public class Lecture : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? FilePath { get; set; }
    public string? Provider {  get; set; }
    public DateTime PublishedAt { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
