using Wasleh.Domain.Entities;

namespace Wasleh.Dtos.Outgoing;

public class ResponseLectureDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? FilePath { get; set; }
    public string? Provider { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime PublishedAt { get; set; }
    public int CourseId { get; set; }
    public int UserId { get; set; }
}
