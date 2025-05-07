using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ElysSalon2._0.domain.Entities;

public class Expense
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } // Primary Key

    [NotNull] public decimal Amount { get; set; }

    [NotNull] public string Reason { get; set; }

    [NotNull] public DateTime Date { get; set; } // You might include the date of the expense
}