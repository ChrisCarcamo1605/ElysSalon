using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Core.Domain.Entities;

public class Expense
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } // Primary Key

    [NotNull] public decimal Amount { get; set; }

    [NotNull] public string Reason { get; set; }

    [NotNull] public DateTime Date { get; set; } // You might include the date of the expense
}