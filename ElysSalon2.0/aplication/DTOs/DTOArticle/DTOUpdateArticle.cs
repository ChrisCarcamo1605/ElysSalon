using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.DTOs.DTOArticle;

public record DTOUpdateArticle(
    Article article,
    IArticleRepository articleRepository,
    IArticleTypeRepository typeRepository
);