using System.Globalization;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.DTOs.DTOSales;

public class DtoSalesList
{
    public string Id { get; set; }
    public string Day { get; set; }
    public DateTime Date { get; set; }
    public String Reason { get; set; }
    public decimal TotalAmount { get; set; }

    public DtoSalesList()
    {
    }
    public DtoSalesList(string day, DateTime date, decimal totalAmount)
    {
        Id = string.Empty;
        Day = day;
        Date = date;
        TotalAmount = totalAmount;
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

    public DtoSalesList(Expense expense)
    {
        Id = expense.Id.ToString();
        Day = expense.Date.ToString("dddd", new CultureInfo("es-SV"));
        Reason = expense.Reason;
        Date = expense.Date;
        TotalAmount = expense.Amount;
    }
}