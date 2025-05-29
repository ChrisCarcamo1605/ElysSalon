using Core.Common;
using Core.Domain.Entities;

namespace Core.Interfaces.Services;

public interface IArtTypeService
{
    Task<ResultFromService> AddTypeAsync(string typeName);
    Task<ResultFromService> EditTypeAsync(ArticleType type);
    Task<ResultFromService> DeleteTypeAsync(int id);
    Task<ResultFromService> GetTypeAsync(int id);
    Task<ResultFromService> GetTypesAsync();
}