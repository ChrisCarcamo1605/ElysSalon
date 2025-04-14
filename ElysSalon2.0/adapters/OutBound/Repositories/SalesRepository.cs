using System.Collections.ObjectModel;
using ElysSalon2._0.adapters.OutBound.DataBase;
using ElysSalon2._0.aplication.Interfaces.Repositories;
using ElysSalon2._0.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElysSalon2._0.adapters.OutBound.Repositories;

public class SalesRepository : ISalesRepository
{
    private readonly ElyDbContext _context;

    public SalesRepository(ElyDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task SavesSale(Sales sale)
    {
        _context.Add(sale);
        _context.SaveChanges();
    }


    public async Task<ObservableCollection<Sales>> GetSalesAsync()
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