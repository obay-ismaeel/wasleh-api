﻿using Wasleh.Domain.Entities;

namespace Wasleh.Dtos.Incoming;

public record RequestQuestionDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
    public int UserId { get; set; }
}
