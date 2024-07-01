namespace Wasleh.Dtos.Outgoing;

public record ResponseCourseDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int FacultyId { get; set; }
    public DateTime CreatedAt { get; set; }
}
