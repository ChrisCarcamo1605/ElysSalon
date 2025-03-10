﻿namespace ElysSalon2._0.aplication.DTOs.DTOArticle;

public record DTOUpdateArticle(
    int ArticleId,
    string Name,
    int ArticleTypeId,
    decimal PriceCost,
    decimal PriceBuy,
    int Stock,
    string Description
);