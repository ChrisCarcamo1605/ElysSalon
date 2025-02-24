using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElysSalon2._0.domain.Entities;

public class Ticket {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string ticketId { get; set; }
    public DateTime emissionDateTime { get; set; }
    public string issuer { get; set; }
    public decimal totalOutTaxes { get; set; }
    public decimal totalWithTaxes { get; set; }
    public decimal totalAmount { get; set; }

    public ICollection<Ticket> TicketDetails { get; set; }
    public Ticket(){}
}