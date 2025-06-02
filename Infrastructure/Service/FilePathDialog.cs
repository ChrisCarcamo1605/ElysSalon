using Core.Interfaces;

namespace Infrastructure.Service;

public class FilePathDialog : IFilePathProvider
{
    public Task<string?> ShowSaveFileDialogAsync(DateTime fromDate, DateTime untilDate)
    {
        throw new NotImplementedException();
    }

    public  string GetReportsDirectory()
    {
        var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var folderPath = Path.Combine(documentsDirectory, "Mis Reportes");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            Console.WriteLine($"Carpeta creada en: {folderPath}");
        }

        return folderPath;
    }
}