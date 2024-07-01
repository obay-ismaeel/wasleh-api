namespace Wasleh.Dtos.Incoming;

public record RequestFacultyDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int UniversityId { get; set; }
}
