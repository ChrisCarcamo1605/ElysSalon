using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElysSalon2._0.domain.Entities;

public class ArticleType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ArticleTypeId { get; set; }

    public string Name { get; set; }
}