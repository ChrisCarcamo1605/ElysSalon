﻿namespace ElysSalon2._0.Core.aplication.DTOs.DTOSales;

public record DtoMonthFinancialData(
    string month,
    decimal week1Total,
    decimal week2Total,
    decimal week3Total,
    decimal week4Total);