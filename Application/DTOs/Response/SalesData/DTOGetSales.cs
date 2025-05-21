namespace Application.DTOs.Response.SalesData;

public record DTOGetSales(
    int SaleId,
    DateTime Date,
    decimal Total
);