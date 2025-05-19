namespace Application.DTOs.Request.Reports;

public record DtoMonthFinancialData(
    string month,
    decimal week1Total,
    decimal week2Total,
    decimal week3Total,
    decimal week4Total);