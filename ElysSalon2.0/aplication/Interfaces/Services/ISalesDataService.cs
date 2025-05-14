using ElysSalon2._0.domain.Entities;
using ElysSalon2._0.domain.Services;
using System.Collections.ObjectModel;

namespace ElysSalon2._0.aplication.Interfaces.Services;

public interface ISalesDataService
{
    Task Add<T>(T obj);
    Task<ResultFromService> Delete<T>(string id);
    Task<ObservableCollection<T>> GetAllOf<T>() where T : class;

}