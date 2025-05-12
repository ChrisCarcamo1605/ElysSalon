using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ElysSalon2._0.aplication.DTOs.DTOSales;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ElysSalon2._0.domain.Services
{
    static class FilterByRangeService
    {
        public static List<decimal> FilterByDateRange(ObservableCollection<DtoSalesList> sales, string range)
        {
            switch (range)
            {
                case "Ultimos 7 dias":

                    return sales.Where(x =>
                            x.Date.Date.AddHours(23).AddMinutes(59).AddSeconds(59) >
                            DateTime.Now.AddDays(-6).Date.AddHours(0))
                        .GroupBy(x => x.Date).OrderBy(x => x.Key)
                        .Select(x => x.Sum(x => x.TotalAmount)).ToList();
                    break;
                case "Ultimo mes":
                    return sales.Where(x => x.Date.Date > DateTime.Now.AddDays(-30).Date)
                        .GroupBy(x => x.Date).OrderBy(x => x.Key)
                        .Select(x => x.Sum(x => x.TotalAmount)).ToList();
                    break;
                case "Ultimos 3 meses":
                    DateTime startDate = DateTime.Now.AddDays(-90).Date;
                    return sales.Where(x => x.Date.Date >= startDate).GroupBy(x => x.Date).OrderBy(x => x.Key)
                        .Select(x => x.Sum(x => x.TotalAmount)).ToList();

                    break;
            }

            return null;
        }
    }
}