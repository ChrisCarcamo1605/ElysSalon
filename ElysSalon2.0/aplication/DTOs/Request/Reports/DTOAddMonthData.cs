namespace ElysSalon2._0.aplication.DTOs.Request.Report;

public record DtoMonthFinancialData(
    string month,
    decimal week1Total,
    decimal week2Total,
    decimal week3Total,
    decimal week4Total);