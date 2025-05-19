using Core.Common;
namespace Core.Interfaces.Services;

public interface ISalesDataService
{
    Task<ResultFromService> Add<T>(T obj);
    Task<ResultFromService> AddRange<T>(List<T> obj);
    Task<ResultFromService> Delete<T>(string id);
    Task<ResultFromService> GetAllOf<T>() where T : class;

    Task<ResultFromService> GetLastId<T>();
}