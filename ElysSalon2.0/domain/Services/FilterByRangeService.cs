using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ElysSalon2._0.adapters.InBound.ViewModels;
using ElysSalon2._0.aplication.DTOs.DTOSales;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ElysSalon2._0.domain.Services
{
    static class FilterByRangeService
    {
        public static List<DtoSalesList> FilterByDateRange(ObservableCollection<DtoSalesList> sales, RangeFilter range)
        {
            DateTime startDate = DateTime.Now.Date;

            switch (range)
            {
                case RangeFilter.LastSevenDays:

                    return sales.Where(x => x.Date.Date.AddHours(23).AddMinutes(59).AddSeconds(59) >=
                                            startDate.AddDays(-6).Date.AddHours(0))
                        .GroupBy(x => x.Date).OrderBy(x => x.Key)
                        .Select(x => new DtoSalesList
                        {
                            Date = x.Key,
                            TotalAmount = x.Sum(x => x.TotalAmount)
                        }).ToList();
                    break;
                case RangeFilter.LastMonth:
                    return sales.Where(x => x.Date.Date >= startDate.AddDays(-30).Date)
                        .GroupBy(x => x.Date).OrderBy(x => x.Key)
                        .Select(x => new DtoSalesList
                        {
                            Date = x.Key,
                            TotalAmount = x.Sum(x => x.TotalAmount)
                        }).ToList();
                    break;

                case RangeFilter.LastThreeMonths:

                    return sales.Where(x => x.Date.Date >= startDate.AddDays(-90).Date)
                        .GroupBy(x => x.Date).OrderBy(x => x.Key)
                        .Select(x => new DtoSalesList
                        {
                            Date = x.Key,
                            TotalAmount = x.Sum(x => x.TotalAmount)
                        }).ToList();

                    break;
            }

            return null;
        }
    }
}