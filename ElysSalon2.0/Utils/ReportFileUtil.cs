using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace ElysSalon2._0.Utils;

public class ReportFileUtil
{
    private static string GetUniqueFileNameInDirectory(string directory, string fileName)
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

    private static string GetUniqueFileName(string filePath)
    {
        if (!File.Exists(filePath)) return filePath;

        var directory = Path.GetDirectoryName(filePath);
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        var extension = Path.GetExtension(filePath);

        var regex = new Regex(@"(.*)\((\d+)\)$");
        var match = regex.Match(fileName);

        string baseName;
        int counter;

        if (match.Success)
        {
            baseName = match.Groups[1].Value.TrimEnd();
            counter = int.Parse(match.Groups[2].Value) + 1;
        }
        else
        {
            baseName = fileName;
            counter = 1;
        }

        string newName;
        string newPath;

        do
        {
            newName = $"{baseName} ({counter})";
            newPath = Path.Combine(directory, newName + extension);
            counter++;
        } while (File.Exists(newPath));

        return newPath;
    }

    public static SaveFileDialog CreateSaveFileDialog(DateTime fromDate, DateTime untilDate)
    {
        string filter = "Archivos Excel (*.xlsx)|*.xlsx";
        var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var folderName = "Mis Reportes";
        var folderPath = Path.Combine(documentsDirectory, folderName);
        var baseName =
            $"Reporte_{fromDate.ToString("ddMMMMyyyy", new CultureInfo("es-ES"))}_Hasta_{untilDate.ToString("ddMMMMyyyy", new CultureInfo("es-ES"))}.xlsx";

        var uniqueName = GetUniqueFileNameInDirectory(folderPath, baseName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            Console.WriteLine($"Carpeta creada en: {folderPath}");
        }

        return new SaveFileDialog
        {
            Filter = filter,
            Title = "Guardar Reporte",
            DefaultExt = "xlsx",
            InitialDirectory = folderPath,
            FileName = Path.GetFileName(uniqueName),
            AddExtension = true
        };
    }
}