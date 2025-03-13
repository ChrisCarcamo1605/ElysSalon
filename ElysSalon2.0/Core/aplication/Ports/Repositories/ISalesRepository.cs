using System.Collections.ObjectModel;
using ElysSalon2._0.Core.domain.Entities;

namespace ElysSalon2._0.Core.aplication.Ports.Repositories;

public interface ISalesRepository
{
    Task SavesSale(ObservableCollection<Sales> observableCollection);
    Task<ObservableCollection<Sales>> GetSales();
    Task<Sales> GetSale(int id);
    Task UpdateSale(Sales sale);
    Task DeleteSale(Sales sale);
}