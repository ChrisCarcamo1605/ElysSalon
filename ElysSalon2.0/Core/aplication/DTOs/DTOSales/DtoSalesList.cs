using System.Globalization;
using System.Numerics;
using ElysSalon2._0.Core.domain.Entities;

namespace ElysSalon2._0.Core.aplication.DTOs.DTOSales;

public class DtoSalesList
{
    public string Id { get; set; }
    public string Day { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }


    public DtoSalesList()
    {
    }

    public DtoSalesList(Sales sales)
    {
        Id = sales.SaleId.ToString();
        Day = sales.SaleDate.ToString("dddd", new CultureInfo("es-SV"));
        Date = sales.SaleDate;
        TotalAmount = sales.Total;
    }
    public DtoSalesList(Ticket ticket)
    {
        Id = ticket.TicketId;
        Day = ticket.EmissionDateTime.ToString("dddd", new CultureInfo("es-SV"));
        Date = ticket.EmissionDateTime;
        TotalAmount = ticket.TotalAmount;
    }
}