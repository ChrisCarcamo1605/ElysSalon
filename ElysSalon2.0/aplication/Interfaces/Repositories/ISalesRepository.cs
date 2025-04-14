using System.Collections.ObjectModel;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Interfaces.Repositories;

public interface ISalesRepository
{
    Task SavesSale(Sales sale);
    Task<ObservableCollection<Sales>> GetSalesAsync();
    Task<Sales> GetSale(int id);
    Task UpdateSale(Sales sale);
    Task DeleteSale(Sales sale);
}