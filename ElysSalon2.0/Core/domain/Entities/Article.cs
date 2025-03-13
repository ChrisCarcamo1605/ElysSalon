using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElysSalon2._0.Core.domain.Entities;

public class Article
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ArticleId { get; set; }

    public string Name { get; set; }
    public int ArticleTypeId { get; set; }
    public decimal PriceCost { get; set; }
    public decimal PriceBuy { get; set; }
    public int Stock { get; set; }

    public string? Description { get; set; }


    [ForeignKey("ArticleTypeId")] public virtual ArticleType ArticleType { get; set; }
}