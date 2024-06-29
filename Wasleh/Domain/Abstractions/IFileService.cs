namespace Wasleh.Domain.Abstractions;

public interface IFileService
{
    Task<string> StoreAsync(IFormFile file);
    void Delete(string filePath);
}
