using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.DTOs.DTOTicketDetails;

public class DtoCreateTicketDetails
{

    public int TicketDetailsId { get; set; }
    public Ticket Ticket { get; set; }
    public Article Article { get; set; }
    public int quantity { get; set; }
    public decimal price { get; set; }


    public DtoCreateTicketDetails(int TicketDetailsId, Ticket Ticket, Article Article, int quantity, decimal price)
    {
        this.TicketDetailsId = TicketDetailsId;
        this.Ticket = Ticket;
        this.Article = Article;
        this.quantity = quantity;
        this.price = price;
    }
};