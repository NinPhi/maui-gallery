namespace MauiGallery.Services;

public class ImageManager
{
    public bool TryDelete(string fullPath)
    {
        if (string.IsNullOrWhiteSpace(fullPath)) return false;
        if (!File.Exists(fullPath)) return false;

        try
        {
            File.Delete(fullPath);
            return true;
        }
        catch { }

        return false;
    }

    public async Task<List<FileResult>> SaveMultipleAsync()
    {
        try
        {
            var files = await PickMultipleAsync();

            if (files == null || !files.Any()) return null;

            foreach (var file in files)
            {
                if (string.IsNullOrWhiteSpace(file.FullPath))
                {
                    files.Remove(file);
                    continue;
                }

                var bytes = await File.ReadAllBytesAsync(file.FullPath);

                var path = FindUniqueFullPath(file.FullPath);

                await File.WriteAllBytesAsync(path, bytes);
            }

            return files;
        }
        catch { }

        return null;
    }

    private async Task<List<FileResult>> PickMultipleAsync()
    {
        try
        {
            var results = await FilePicker.Default.PickMultipleAsync(PickOptions.Images);

            if (results != null && results.Any())
            {
                foreach (var result in results)
                {
                    using var stream = await result.OpenReadAsync();
                    var image = ImageSource.FromStream(() => stream);
                }
            }

            return results?.ToList();
        }
        catch { }

        return null;
    }

    private string FindUniqueFullPath(string fullPath, string targetDirectory = null)
    {
        if (string.IsNullOrWhiteSpace(targetDirectory))
            targetDirectory = FileSystem.AppDataDirectory;
        else if(!Directory.Exists(targetDirectory))
            Directory.CreateDirectory(targetDirectory);

        string path = Path.Combine(targetDirectory, Path.GetFileName(fullPath));

        int count = 0;
        while (true)
        {
            if (!File.Exists(path)) break;

            path = Path.Combine(targetDirectory,
                Path.GetFileNameWithoutExtension(fullPath)
                + $" ({++count})" + Path.GetExtension(fullPath));
        }

        return path;
    }
}
