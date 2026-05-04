using BuildingStore.Web.Models;
using BuildingStore.Web.Repositories;

namespace BuildingStore.Web.Services;

public class MaterialService : IMaterialService 
{
    private readonly IMaterialRepository _repository;

    public MaterialService(IMaterialRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<BuildingMaterial>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<BuildingMaterial?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddOrUpdateAsync(BuildingMaterial material)
    {
        if (material.Id == 0)
        {
            await _repository.AddAsync(material);
        }
        else
        {
            await _repository.UpdateAsync(material);
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
