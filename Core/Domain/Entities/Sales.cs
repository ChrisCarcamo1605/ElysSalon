using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Core.Domain.Entities;

public class Sales
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SaleId { get; set; }

    [NotNull] public DateTime SaleDate { get; set; }
    [NotNull] public decimal Total { get; set; }
}