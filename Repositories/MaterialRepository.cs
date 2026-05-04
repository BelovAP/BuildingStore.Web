using BuildingStore.Web.Data;
using BuildingStore.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildingStore.Web.Repositories;

public class MaterialRepository : IMaterialRepository 
{
    private readonly StoreContext _context;

    public MaterialRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task<List<BuildingMaterial>> GetAllAsync()
    {
        return await _context.Materials
            .Include(m => m.Category)
            .Include(m => m.Suppliers).ThenInclude(ms => ms.Supplier)
            .ToListAsync();
    }

    public async Task<BuildingMaterial?> GetByIdAsync(int id)
    {
        return await _context.Materials
            .Include(m => m.Spec)
            .Include(m => m.Category)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task AddAsync(BuildingMaterial material)
    {
        _context.Materials.Add(material);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(BuildingMaterial material)
    {
        _context.Materials.Update(material);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var material = await _context.Materials.FindAsync(id);
        if (material != null)
        {
            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();
        }
    }
}
