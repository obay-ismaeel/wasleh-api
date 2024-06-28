using Microsoft.AspNetCore.Mvc;

namespace Wasleh.Domain.Abstractions;

public interface IReadWriteActions <T> where T : class
{
    Task<IActionResult> Get();
    Task<IActionResult> Get(int id);
    Task<IActionResult> Post(T value);
    Task<IActionResult> Put(int id, T value);
    Task<IActionResult> Delete(int id);
}