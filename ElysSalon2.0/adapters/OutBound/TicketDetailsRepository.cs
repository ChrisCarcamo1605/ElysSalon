using ElysSalon2._0.aplication.DTOs.DTOTicketDetails;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Utils;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.adapters.OutBound;

public class TicketDetailsRepository : ITicketDetailsRepository
{
    public void createTicketDetails(TicketDetails ticket)
    {
        var db = DbUtil.getInstance();
        Dictionary<string, object> Dic = new Dictionary<string, object>
        {
            {"ticketDetailsId", ticket.ticketDetailsId},
            {"ticketId", ticket.ticket},
            {"articleId", ticket.article.articleId},
            {"quantity", ticket.quantity},
            {"amount", ticket.price}
        };
        db.AddToDb<TicketDetails>("ticketDetails",Dic);
    }

    public List<TicketDetails> GetTicketDetails(string ticketId)
    {
        throw new NotImplementedException();
    }
}