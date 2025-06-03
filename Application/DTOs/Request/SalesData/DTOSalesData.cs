using System.Globalization;
using Application.DTOs.Response.Expenses;
using Application.DTOs.Response.TicketDetails;
using Core.Domain.Entities;

namespace Application.DTOs.Request.SalesData;

public record DTOSalesData(
    string Id,
    string Day,
    DateTime Date,
    string Reason,
    decimal TotalAmount,
    Article article
)
{
    public DTOSalesData() : this(string.Empty, string.Empty, DateTime.MinValue, string.Empty, 0, null)
    {
    }

    public DTOSalesData(string day, DateTime date, decimal totalAmount) : this(string.Empty, day, date, string.Empty,
        totalAmount, null)
    {
    }

    public DTOSalesData(Sales sales) : this(
        sales.SaleId.ToString(),
        sales.SaleDate.ToString("dddd", new CultureInfo("es-SV")),
        sales.SaleDate,
        string.Empty,
        sales.Total, null
    )
    {
    }

    public DTOSalesData(Ticket ticket) : this(
        ticket.TicketId,
        ticket.EmissionDateTime.ToString("dddd", new CultureInfo("es-SV")),
        ticket.EmissionDateTime,
        string.Empty, // Reason no está presente en Ticket
        ticket.TotalAmount, null
    )
    {
    }

    public DTOSalesData(DTOGetExpense expense) : this(
        expense.ExpenseId.ToString(),
        expense.Date.ToString("dddd", new CultureInfo("es-SV")),
        expense.Date,
        expense.Reason,
        expense.Amount, null
    )
    {
    }

    public DTOSalesData(DTOGetTicketDetails ticketDetails) : this(
        ticketDetails.TicketDetailsId.ToString(),
        ticketDetails.Ticket.EmissionDateTime.ToString("dddd", new CultureInfo("es-SV")),
        ticketDetails.Ticket.EmissionDateTime,
        string.Empty,
        ticketDetails.TotalPrice, ticketDetails.Article
    )
    {
    }
}