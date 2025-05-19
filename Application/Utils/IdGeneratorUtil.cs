namespace Application.Utils;

public class IdGeneratorUtil
{
    private const string PREFIX = "T";
    private const string DEFAULT_ID = "T-000100";
    private const char SEPARATOR = '-';
    private const int NUMBER_DIGITS = 6; // Para generar 000100, 000101, etc.

    public static string GenerateNewId(string lastId)
    {
        if (!IsValidIdFormat(lastId)) return DEFAULT_ID;

        try
        {
            var parts = lastId.Split(SEPARATOR);
            if (parts.Length != 2 || parts[0] != PREFIX)
                return DEFAULT_ID;

            if (!int.TryParse(parts[1], out var currentNumber))
                return DEFAULT_ID;
            currentNumber++;

            return $"{PREFIX}{SEPARATOR}{currentNumber.ToString($"D{NUMBER_DIGITS}")}";
        }
        catch
        {
            return DEFAULT_ID;
        }
    }

    public static bool IsValidIdFormat(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return false;

        var parts = id.Split(SEPARATOR);
        if (parts.Length != 2 || parts[0] != PREFIX)
            return false;

        return parts[1].Length == NUMBER_DIGITS &&
               parts[1].All(char.IsDigit);
    }

    // Versión para inicializar con el último número
    public static string GenerateNextId(int lastNumber)
    {
        lastNumber++;
        return $"{PREFIX}{SEPARATOR}{lastNumber.ToString($"D{NUMBER_DIGITS}")}";
    }
}