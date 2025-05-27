using Core.Common;
using Core.Domain.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Validations;

namespace Core.Services;

public class ArtTypeService : IArtTypeService
{
    private readonly IRepository<ArticleType> _typeRepository;

    public ArtTypeService(IRepository<ArticleType> repository)
    {
        _typeRepository = repository;
    }

    public async Task<ResultFromService> AddTypeAsync(string name)
    {
        var _typesCollection = await _typeRepository.GetAllAsync();
        var validate = ArticleValidations.ValidateAddType(name, _typesCollection);

        if (validate.Success is false) return validate;

        await _typeRepository.SaveAsync(new ArticleType { Name = name });
        reloadItems?.Invoke();
        return ResultFromService.SuccessResult("Tipo creado correctamente");
    }

    public async Task<ResultFromService> EditTypeAsync(ArticleType type)
    {
        var articleType =
            await _typeRepository.FindAsync(x => x.Name != type.Name && x.ArticleTypeId == type.ArticleTypeId);

        if (articleType == null) return ResultFromService.Failed("Tipo ya existente");

        var validated = ArticleValidations.ValidateUpdateType(type.Name, articleType);
        await _typeRepository.UpdateAsync((ArticleType)validated.Data);

        return ResultFromService.SuccessResult(validated.Data, "Tipo actualizado correctamente");
    }

    public async Task<ResultFromService> DeleteTypeAsync(int id)
    {
        if (id == 0) return ResultFromService.Failed("Seleccione un artículo para eliminar");

        await _typeRepository.DeleteAsync(await _typeRepository.FindAsync(x => x.ArticleTypeId == id));
        reloadItems?.Invoke();
        return ResultFromService.SuccessResult("Tipo eliminado correctamente");
    }

    public async Task<ResultFromService> getTypeAsync(string name)
    {
        try
        {
            var result = await _typeRepository.FindAsync(x => x.Name == name);
            return ResultFromService.SuccessResult(result, "Tipo encontrado");
        }
        catch (Exception e)
        {
            return ResultFromService.SuccessResult("Tipo no encontrado, error: " + e.Message);
        }
    }

    public async Task<ResultFromService> GetTypesAsync()
    {
        try
        {
            var result = await _typeRepository.GetAllAsync();
            return ResultFromService.SuccessResult(result, "tipos obtenidos!");
        }
        catch (Exception e)
        {
            return ResultFromService.SuccessResult("hubo un error: " + e.Message);
        }
    }

    public event Action reloadItems;
    public event Action clearForms;
}