namespace Wasleh.Dtos.Incoming;

public record RequestUniversityDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public IFormFile? LogoFile { get; set; }
    public string? Country { get; set; }
}
