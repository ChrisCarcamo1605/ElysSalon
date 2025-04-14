using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElysSalon2._0.domain.Entities;

public class Ticket
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string TicketId { get; set; }

    public DateTime EmissionDateTime { get; set; }
    public string Issuer { get; set; }
    public decimal? TotalOutTaxes { get; set; }
    public decimal? TotalWithTaxes { get; set; }
    public decimal TotalAmount { get; set; }


    public virtual ICollection<TicketDetails> TicketDetails { get; set; } = new List<TicketDetails>();
}