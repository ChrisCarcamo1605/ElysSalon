using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using Microsoft.EntityFrameworkCore;

namespace ElysSalon2._0.domain.Entities;

public class Article {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int articleId { get; set; }
    public string articleName { get; set; }
    public int articleTypeId { get; set; }
    public decimal priceCost { get; set; }
    public decimal priceBuy { get; set; }
    public int stock { get; set; }
    public string description { get; set; }

    [ForeignKey("articleTypeId")]
    public virtual ArticleType ArticleType { get; set; }
    public ICollection<Article> Articles { get; private set; }


   
}