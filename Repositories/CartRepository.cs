using BuildingStore.Web.Data;
using BuildingStore.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildingStore.Web.Repositories;

public class CartRepository : ICartRepository
{
    private readonly StoreContext _context;
    public CartRepository(StoreContext context) => _context = context;

    public async Task<Cart?> GetByUserIdAsync(int userId) =>
        await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);

    public async Task<Cart> CreateAsync(int userId)
    {
        var cart = new Cart { UserId = userId };
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();
        return cart;
    }

    public async Task AddOrUpdateItemAsync(CartItem item)
    {
        var existing = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.CartId == item.CartId && ci.MaterialId == item.MaterialId);

        if (existing != null)
        {
            existing.Quantity += item.Quantity;
        }
        else
        {
            _context.CartItems.Add(item);
        }
        await _context.SaveChangesAsync();
    }

    public async Task RemoveItemAsync(int cartItemId)
    {
        var item = await _context.CartItems.FindAsync(cartItemId);
        if (item != null) { _context.CartItems.Remove(item); await _context.SaveChangesAsync(); }
    }

    public async Task ClearAsync(int cartId)
    {
        var items = await _context.CartItems.Where(ci => ci.CartId == cartId).ToListAsync();
        _context.CartItems.RemoveRange(items);
        await _context.SaveChangesAsync();
    }

    public async Task<Cart> GetWithItemsAsync(int userId)
    {
        return await _context.Carts
            .Include(c => c.Items).ThenInclude(ci => ci.Material)
            .FirstOrDefaultAsync(c => c.UserId == userId)
            ?? new Cart { Items = new() }; 
    }
}
