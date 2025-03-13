using System.Collections.ObjectModel;
using ElysSalon2._0.adapters.OutBound.DataBase;
using ElysSalon2._0.Core.aplication.Ports.Repositories;
using ElysSalon2._0.Core.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElysSalon2._0.adapters.OutBound.Repositories;

public class SalesRepository : ISalesRepository
{
    private readonly ElyDbContext _context;

    public SalesRepository(ElyDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task SavesSale(ObservableCollection<Sales> observableCollection)
    {
        var sale = new Sales
        {
            SaleDate = DateTime.UtcNow,
            Total = 25.52
        };

        _context.Add(sale);
        _context.SaveChanges();
    }

    public async Task<ObservableCollection<Sales>> GetSales()
    {
        var list = await _context.Sales.ToListAsync();
        return new ObservableCollection<Sales>(list);
    }

    public Task<Sales> GetSale(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateSale(Sales sale)
    {
        throw new NotImplementedException();
    }

    public Task DeleteSale(Sales sale)
    {
        throw new NotImplementedException();
    }
}