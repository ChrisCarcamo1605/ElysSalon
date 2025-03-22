using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElysSalon2._0.Core.domain.Entities;

public class Sales
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SaleId { get; set; }

    public DateTime SaleDate { get; set; }
    public decimal Total { get; set; }
}