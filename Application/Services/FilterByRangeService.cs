using System.Collections.ObjectModel;
using Application.DTOs.Request.SalesData;
using Application.Enums;

namespace Application.Services;

public static class FilterByRangeService
{
    public static List<DTOSalesData> FilterByDateRange(ObservableCollection<DTOSalesData> sales, RangeFilter range)
    {
        var startDate = DateTime.Now.Date;

        switch (range)
        {
            case RangeFilter.LastSevenDays:

                return sales.Where(x => x.Date.Date.AddHours(23).AddMinutes(59).AddSeconds(59) >=
                                        startDate.AddDays(-6).Date.AddHours(0))
                    .GroupBy(x => x.Date).OrderBy(x => x.Key)
                    .Select(x => new DTOSalesData
                    {
                        Date = x.Key,
                        TotalAmount = x.Sum(x => x.TotalAmount)
                    }).ToList();
                break;
            case RangeFilter.LastMonth:
                return sales.Where(x => x.Date.Date >= startDate.AddDays(-30).Date)
                    .GroupBy(x => x.Date).OrderBy(x => x.Key)
                    .Select(x => new DTOSalesData
                    {
                        Date = x.Key,
                        TotalAmount = x.Sum(x => x.TotalAmount)
                    }).ToList();
                break;

            case RangeFilter.LastThreeMonths:

                return sales.Where(x => x.Date.Date >= startDate.AddDays(-90).Date)
                    .GroupBy(x => x.Date).OrderBy(x => x.Key)
                    .Select(x => new DTOSalesData
                    {
                        Date = x.Key,
                        TotalAmount = x.Sum(x => x.TotalAmount)
                    }).ToList();

                break;
        }

        return null;
    }

    public static decimal GetTotalFrom(ObservableCollection<DTOSalesData> items, RangeFilter filter)
    {
        return filter switch
        {
            RangeFilter.LastSevenDays => items.Where(x => x.Date.Date.AddHours(23).AddMinutes(59).AddSeconds(59) >=
                                                          DateTime.Now.Date.AddDays(-6).Date.AddHours(0))
                .Sum(x => x.TotalAmount),
            RangeFilter.LastMonth => items.Where(x => x.Date.Date >= DateTime.Now.Date.AddDays(-30).Date)
                .Sum(x => x.TotalAmount),
            RangeFilter.LastThreeMonths => items.Where(x => x.Date.Date >= DateTime.Now.Date.AddDays(-90).Date)
                .Sum(x => x.TotalAmount),
            _ => items.Sum(x => x.TotalAmount)
        };
    }
}