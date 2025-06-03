using Core.Interfaces;

namespace Infrastructure.Utils;

public class FilePathDialogUtil : IFilePathProvider
{
    public Task<string?> ShowSaveFileDialogAsync(DateTime fromDate, DateTime untilDate)
    {
        throw new NotImplementedException();
    }

    public string GetReportsDirectory()
    {
        //var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var folderPath = Path.Combine("C:\\Users\\Christian\\Documents", "Mis Reportes");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            Console.WriteLine($"Carpeta creada en: {folderPath}");
        }

        return folderPath;
    }
}