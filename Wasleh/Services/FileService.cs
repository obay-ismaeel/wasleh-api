using Wasleh.Domain.Abstractions;
using Wasleh.Domain.Settings;

namespace Wasleh.Services;

public class FileService : IFileService
{
    public async Task<string> StoreAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return null;
        }

        var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

        var filePath = Path.Combine(FileSettings.ImagesPath, uniqueFileName);

        var fullPath = Path.Combine(FileSettings.WebRootPath, filePath);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return filePath;
    }

    public void Delete(string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return;

        var fullPath = Path.Combine(FileSettings.WebRootPath, filePath);
        File.Delete(fullPath);
    }
}
