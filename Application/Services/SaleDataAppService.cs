using Core.Common;
using Core.Domain.Entities;
using Core.Interfaces.Services;
using System.Collections.ObjectModel;
using Application.DTOs.Request.SalesData;

namespace Application.Services;

public class SaleDataAppService
{
    private readonly ISalesDataService _dataService;

    public SaleDataAppService(ISalesDataService dataService)
    {
        _dataService = dataService;
    }

    public async Task<ResultFromService> Add<T>(T obj)
    {
        return await _dataService.Add(obj);
    }

    public async Task<ResultFromService> AddRange<T>(List<T> objects)
    {
       return await _dataService.AddRange(objects);
    }

    public async Task<ResultFromService> Delete<T>(string id)
    {
        return await _dataService.Delete<T>(id);
    }

    public async Task<ResultFromService> GetAllOf<T>() where T : class
    {
        return await _dataService.GetAllOf<T>();
    }

    public async Task<ResultFromService> GetLastId<T>()
    {
        try
        {
            var lastTicket = await _dataService.GetLastId<T>();
            return ResultFromService.SuccessResult(lastTicket, "ID generado exitosamente");
        }
        catch (Exception ex)
        {
            return ResultFromService.Failed("Error al generar el nuevo ID");
        }
    }
}