namespace Core.Interfaces;

public interface IFilePathProvider
{
    Task<string?> ShowSaveFileDialogAsync(DateTime fromDate, DateTime untilDate);
     string GetReportsDirectory();
}