using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using Core.Interfaces;
using Microsoft.Win32;

namespace ElysSalon2._0.Utils;

public class ReportFileUtil : IFilePathProvider
{
    public Task<string?> ShowSaveFileDialogAsync(DateTime fromDate, DateTime untilDate)
    {
        var saveDialog = new SaveFileDialog
        {
            Filter = "Archivos Excel (*.xlsx)|*.xlsx",
            Title = "Guardar Reporte",
            DefaultExt = "xlsx",
            InitialDirectory = GetReportsDirectory(),
            FileName = GetDefaultFileName(fromDate, untilDate),
            AddExtension = true
        };

        // Show dialog and return result
        return Task.FromResult(saveDialog.ShowDialog() == true ? saveDialog.FileName : null);
    }

    public string GetReportsDirectory()
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

    private string GetDefaultFileName(DateTime fromDate, DateTime untilDate)
    {
        var baseName =
            $"Reporte_{fromDate.ToString("ddMMMMyyyy", new CultureInfo("es-ES"))}_Hasta_{untilDate.ToString("ddMMMMyyyy", new CultureInfo("es-ES"))}.xlsx";
        var folderPath = GetReportsDirectory();

        return GetUniqueFileName(folderPath, baseName);
    }


    private string GetUniqueFileName(string directory, string fileName)
    {
        var fullPath = Path.Combine(directory, fileName);

        if (!File.Exists(fullPath)) return fileName;

        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        var extension = Path.GetExtension(fileName);

        var regex = new Regex(@"(.*)\((\d+)\)$");
        var match = regex.Match(fileNameWithoutExtension);

        string baseName;
        int counter;

        if (match.Success)
        {
            baseName = match.Groups[1].Value.TrimEnd();
            counter = int.Parse(match.Groups[2].Value) + 1;
        }
        else
        {
            baseName = fileNameWithoutExtension;
            counter = 1;
        }

        string newName;
        string newPath;

        do
        {
            newName = $"{baseName} ({counter}){extension}";
            newPath = Path.Combine(directory, newName);
            counter++;
        } while (File.Exists(newPath));

        return newName;
    }
}