using System.Security.RightsManagement;
using Windows.UI.Notifications;
using ElysSalon2._0.aplication.DTOs.DTOTicketDetails;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElysSalon2._0.domain.Entities;

public class TicketDetails {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ticketDetailsId { get; set; }
    public string ticketId { get; set; }
    public Article article { get; set; }
    public int quantity { get; set; }
    public decimal price { get; set; }
    public decimal totalPrice { get; set; }

    [ForeignKey("ticketId")]
    public virtual Ticket Ticket { get; set; }
    public ICollection<TicketDetails> tickets { get; set; }

  public TicketDetails(){}

    
}