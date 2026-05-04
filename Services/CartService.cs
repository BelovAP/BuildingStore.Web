using BuildingStore.Web.Models;
using BuildingStore.Web.Repositories;

namespace BuildingStore.Web.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepo;
    public CartService(ICartRepository cartRepo) => _cartRepo = cartRepo;

    public async Task<Cart> GetCartAsync(int userId)
    {
        var cart = await _cartRepo.GetWithItemsAsync(userId);
        return cart.Id == 0 ? cart : cart; 
    }

    public async Task AddToCartAsync(int userId, int materialId, int quantity = 1)
    {
        var cart = await _cartRepo.GetByUserIdAsync(userId);
        if (cart == null) cart = await _cartRepo.CreateAsync(userId);

        var item = new CartItem { CartId = cart.Id, MaterialId = materialId, Quantity = quantity };
        await _cartRepo.AddOrUpdateItemAsync(item);
    }

    public async Task RemoveFromCartAsync(int userId, int cartItemId)
    {
        await _cartRepo.RemoveItemAsync(cartItemId);
    }

    public async Task UpdateQuantityAsync(int userId, int cartItemId, int quantity)
    {
        
        await _cartRepo.RemoveItemAsync(cartItemId);
        
    }

    public async Task ClearCartAsync(int userId)
    {
        var cart = await _cartRepo.GetByUserIdAsync(userId);
        if (cart != null) await _cartRepo.ClearAsync(cart.Id);
    }

    public decimal GetTotalPrice(Cart cart) =>
        cart.Items.Sum(ci => ci.Material.Price * ci.Quantity);
}
