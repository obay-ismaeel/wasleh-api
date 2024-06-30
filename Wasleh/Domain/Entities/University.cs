namespace Wasleh.Domain.Entities;

public class University : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string LogoPath { get; set; }
    public string Country { get; set; }
    public ICollection<Faculty> Faculties { get; set; } = [];
}
