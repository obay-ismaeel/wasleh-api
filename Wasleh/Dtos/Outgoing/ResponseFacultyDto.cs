namespace Wasleh.Dtos.Outgoing;

public record ResponseFacultyDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int UniversityId { get; set; }
    public DateTime CreatedAt { get; set; }
}
