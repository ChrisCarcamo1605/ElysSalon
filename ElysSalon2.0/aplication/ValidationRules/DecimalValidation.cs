using System.Globalization;
using System.Windows.Controls;

namespace ElysSalon2._0.aplication.ValidationRules;

public class DecimalValidation : ValidationRule
{
    public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
    {
        string stringValue = value as string;

        if (string.IsNullOrEmpty(stringValue))
        {
            return new ValidationResult(false, "El campo no puede estar vacio");
        }

        if(!decimal.TryParse(stringValue, NumberStyles.Any, cultureInfo, out decimal result))
            return new ValidationResult(false, "El valor no es un numero valido");

        if (result < 0)
            return new ValidationResult(false, "El valor no puede ser negativo");

        return ValidationResult.ValidResult;
    }
}