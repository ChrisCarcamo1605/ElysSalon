using System.Collections.ObjectModel;
using System.Globalization;
using Application.DTOs.Request.SalesData;

namespace Application.Utils;

public static class CollectionUtil
{
    public static ObservableCollection<DTOSalesData> LoadEmptyDates(ObservableCollection<DTOSalesData> collection)
    {
        var list = collection;
        var minDate = list.Min(x => x.Date);
        var maxDate = DateTime.Now.Date;


        var dates = new HashSet<DateTime>(list.Select(x => x.Date.Date)
            .OrderBy(x => x.Date.Date)
            .ToList());
        var allDates = new List<DateTime>();

        for (var i = minDate; i <= maxDate; i = i.AddDays(1)) allDates.Add(i.Date);

        foreach (var i in allDates)
            if (!dates.Contains(i.Date))
                list.Add(
                    new DTOSalesData(i.ToString("dddd", new CultureInfo("es-ES")),
                        i.Date, 0));

        return list;
    }

    public static List<DTOSalesData> LoadEmptyDates<T>(
        List<T> collection,
        Func<T, DateTime> dateSelector,
        Func<T, decimal> totalSelector) where T : class
    {
        var result = new List<DTOSalesData>();

        if (collection == null || collection.Count == 0)
            return result;

        var minDate = collection.Min(x => dateSelector(x)).Date;
        var maxDate = collection.Max(x => dateSelector(x)).Date;

        // Diccionario de fechas con sus totales reales
        var dateTotalMap = collection
            .GroupBy(x => dateSelector(x).Date)
            .ToDictionary(g => g.Key, g => g.Sum(totalSelector));

        var culture = new CultureInfo("es-ES");

        for (var date = minDate; date <= maxDate; date = date.AddDays(1))
            if (dateTotalMap.TryGetValue(date, out var total))
                result.Add(new DTOSalesData(
                    date.ToString("dddd", culture),
                    date,
                    total
                ));
            else
                result.Add(new DTOSalesData(
                    date.ToString("dddd", culture),
                    date,
                    0
                ));

        return result;
    }
}