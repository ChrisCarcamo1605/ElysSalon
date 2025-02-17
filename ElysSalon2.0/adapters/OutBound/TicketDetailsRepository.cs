using ElysSalon2._0.aplication.DTOs.DTOTicketDetails;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Utils;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.adapters.OutBound;

public class TicketDetailsRepository : ITicketDetailsRepository
{
    public void createTicketDetails(DtoCreateTicketDetails dto)
    {
        var db = DbUtil.getInstance();
        Dictionary<string, object> Dic = new Dictionary<string, object>
        {
            {"ticketDetailsId", dto.TicketDetailsId},
            {"ticketId", dto.Ticket},
            {"articleId", dto.Article.articleId},
            {"quantity", dto.quantity},
            {"amount", dto.price}
        };
        db.AddToDb<TicketDetails>("ticketDetails",Dic);
    }

    public List<DTOGetTicketDetails> GetTicketDetails(string ticketId)
    {
        throw new NotImplementedException();
    }
}