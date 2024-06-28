namespace Wasleh.Domain.Entities;

public class Course : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? FacultyId { get; set; }
    public Faculty? Faculty { get; set; }
}
