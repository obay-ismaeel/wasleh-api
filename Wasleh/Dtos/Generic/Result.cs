
using Wasleh.Dtos.Errors;

namespace Wasleh.Dtos.Generic;

public record Result<T>
{
    public T Content { get; set; }
    public Error Error { get; set; }
    public bool IsSuccessful => Error == null;
    public DateTime ResponseTime { get; set; } = DateTime.UtcNow;
}