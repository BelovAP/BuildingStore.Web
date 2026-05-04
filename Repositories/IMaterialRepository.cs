using BuildingStore.Web.Models;

namespace BuildingStore.Web.Repositories;

public interface IMaterialRepository
{
    Task<List<BuildingMaterial>> GetAllAsync();
    Task<BuildingMaterial?> GetByIdAsync(int id);
    Task AddAsync(BuildingMaterial material);
    Task UpdateAsync(BuildingMaterial material);
    Task DeleteAsync(int id);
}
