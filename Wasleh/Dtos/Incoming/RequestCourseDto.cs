namespace Wasleh.Dtos.Incoming;

public record RequestCourseDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int FacultyId { get; set; }
}
