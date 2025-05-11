using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ElysSalon2._0.aplication.DTOs.DTOSales;

namespace ElysSalon2._0.domain.Services
{
    static class FilterByRangeService
    {
        public static List<decimal> FilterByDateRange(ObservableCollection<DtoSalesList> sales, String range)
        {
            switch (range)
            {
                case "Ultimos 7 dias":

                    return sales.Where(x => x.Date.Date > DateTime.Now.AddDays(-7).Date)
                        .GroupBy(x => x.Date).OrderBy(x => x.Key)
                        .Select(x => x.Sum(x => x.TotalAmount)).ToList();
                    break;
                case "Ultimo mes":
                    return sales.Where(x => x.Date.Date > DateTime.Now.AddDays(-30).Date)
                        .GroupBy(x => x.Date).OrderBy(x => x.Key)
                        .Select(x => x.Sum(x => x.TotalAmount)).ToList();
                    break;
                case "Ultimos 3 meses":
                    return sales.Where(x => x.Date.Date > DateTime.Now.AddDays(-90).Date)
                        .GroupBy(x => x.Date).OrderBy(x => x.Key)
                        .Select(x => x.Sum(x => x.TotalAmount)).ToList();
                    break;
            }

            return null;
        }
    }
}