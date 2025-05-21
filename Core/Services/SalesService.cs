using Core.Common;
using Core.Domain.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Core.Services;

public class SalesService : ISalesService
{
    private readonly IRepository<Sales> _salesRepo;

    public SalesService(IRepository<Sales> salesRepo)
    {
        _salesRepo = salesRepo;
    }

    public async Task<ResultFromService> AddAsync(Sales sales)
    {
        return ResultFromService.SuccessResult(await _salesRepo.SaveAsync(sales),
            "Venta agregada exitosammente");
    }

    public async Task<ResultFromService> DeleteAsync(string id)
    {
        try
        {
            await _salesRepo.DeleteAsync(await _salesRepo.GetByIdAsync(int.Parse(id)));
            return ResultFromService.SuccessResult("Venta eliminada exitosamente");
        }
        catch (Exception e)
        {
            return ResultFromService.Failed("No se pudo eliminar la venta: " + e.Message);
        }
    }

    public async Task<ResultFromService> GetAllOfAsync()
    {
        try
        {
            var sales = await _salesRepo.GetAllAsync();
            return ResultFromService.SuccessResult(sales);
        }
        catch (Exception e)
        {
            return ResultFromService.Failed(e.Message);
        }
    }
}