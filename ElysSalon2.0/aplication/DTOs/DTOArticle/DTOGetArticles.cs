namespace ElysSalon2._0.aplication.DTOs.DTOArticle;

public record DTOGetArticles(int article_id, string article_name, string article_type, decimal price_cost, decimal price_buy, int stock, string description);