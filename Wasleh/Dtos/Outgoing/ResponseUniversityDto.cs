﻿namespace Wasleh.Dtos.Outgoing;

public record ResponseUniversityDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? LogoPath { get; set; }
    public string? Country { get; set; }
    public DateTime CreatedAt { get; set; }
}
