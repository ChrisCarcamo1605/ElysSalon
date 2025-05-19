namespace Application.DTOs.Request.Reports;

public record DTOAddAnualData(
    int year,
    decimal jenuaryTotal,
    decimal februaryTotal,
    decimal marchTotal,
    decimal aprilTotal,
    decimal mayTota,
    decimal juneTotal,
    decimal julyTotal,
    decimal augustTotal,
    decimal septemberTotal,
    decimal octuberTotal,
    decimal novemberTotal,
    decimal decemberTotal);