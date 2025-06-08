using Application.DTOs.Request.TicketsDetails;
using Core.Common;

namespace Application.Interfaces;

public interface ISaleDataAppService
{
    Task<ResultFromService> Add<T>(object obj);
    Task<ResultFromService> AddTicketDetailsRange(List<DtoCreateTicketDetails> ticketDetails);
    Task<ResultFromService> Delete<T>(string id);

    Task<ResultFromService> GetAllOf<T>() where T : class;
}