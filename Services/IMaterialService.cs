using BuildingStore.Web.Models;

namespace BuildingStore.Web.Services;

public interface IMaterialService
{
    Task<List<BuildingMaterial>> GetAllAsync();
    Task<BuildingMaterial?> GetByIdAsync(int id);
    Task AddOrUpdateAsync(BuildingMaterial material);
    Task DeleteAsync(int id);
}
