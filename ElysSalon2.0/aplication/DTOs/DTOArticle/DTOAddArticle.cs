﻿namespace ElysSalon2._0.aplication.DTOs.DTOArticle;

public record DTOAddArticle(
    string Name,
    int ArticleTypeId,
    string PriceCost,
    string PriceBuy,
    string Stock,
    string Description);