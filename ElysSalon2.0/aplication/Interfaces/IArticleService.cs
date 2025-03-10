﻿using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Services;

public interface IArticleService
{
    public event Action clearForms;
    public event Action reloadItems;
    Task<ServiceResult> AddArticle(DTOAddArticle dto);
    Task<ServiceResult> UpdateArticle(DTOUpdateArticle dto);
    Task<ServiceResult> DeleteArticle(int id);
    Task<ServiceResult> AddType(string typeName);
    Task<ServiceResult> EditType(ArticleType type);
    Task<ServiceResult> DeleteType(int id);
}