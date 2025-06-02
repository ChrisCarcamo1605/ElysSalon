namespace Core.Interfaces;

public interface IFileDialogInterface
{
    Task<string?> ShowSaveFileDialogAsync(DateTime fromDate, DateTime untilDate);
}