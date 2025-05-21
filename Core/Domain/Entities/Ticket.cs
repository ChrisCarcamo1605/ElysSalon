using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities;

public class Ticket
{
    [Key] public string TicketId { get; set; }

    public DateTime EmissionDateTime { get; set; }
    public string Issuer { get; set; }
    public decimal? TotalOutTaxes { get; set; }
    public decimal? TotalWithTaxes { get; set; }
    public decimal TotalAmount { get; set; }


    public virtual ICollection<TicketDetails> TicketDetails { get; set; } = new List<TicketDetails>();
}