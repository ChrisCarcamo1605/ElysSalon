using System.Globalization;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.DTOs.Request.SalesData;

public record DTOSalesData(
    string Id,
    string Day,
    DateTime Date,
    string Reason,
    decimal TotalAmount
)
{
    public DTOSalesData() : this(string.Empty, string.Empty, DateTime.MinValue, string.Empty, 0)
    {
    }

    public DTOSalesData(string day, DateTime date, decimal totalAmount) : this(string.Empty, day, date, string.Empty, totalAmount)
    {
    }

    public DTOSalesData(Sales sales) : this(
        sales.SaleId.ToString(),
        sales.SaleDate.ToString("dddd", new CultureInfo("es-SV")),
        sales.SaleDate,
        string.Empty, 
        sales.Total
    )
    {
    }

    public DTOSalesData(Ticket ticket) : this(
        ticket.TicketId,
        ticket.EmissionDateTime.ToString("dddd", new CultureInfo("es-SV")),
        ticket.EmissionDateTime,
        string.Empty, // Reason no está presente en Ticket
        ticket.TotalAmount
    )
    {
    }

    public DTOSalesData(Expense expense) : this(
        expense.Id.ToString(),
        expense.Date.ToString("dddd", new CultureInfo("es-SV")),
        expense.Date,
        expense.Reason,
        expense.Amount
    )
    {
    }
}