namespace Wasleh.Dtos.Incoming;

public record RequestLectureDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public IFormFile? File { get; set; }
    public string? Provider { get; set; }
    public DateTime PublishedAt { get; set; }
    public int CourseId { get; set; }
    public int UserId { get; set; }
}
