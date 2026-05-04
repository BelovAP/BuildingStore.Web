using BuildingStore.Web.Models;

namespace BuildingStore.Web.Repositories;

public interface ICartRepository
{
    Task<Cart?> GetByUserIdAsync(int userId);
    Task<Cart> CreateAsync(int userId);
    Task AddOrUpdateItemAsync(CartItem item);
    Task RemoveItemAsync(int cartItemId);
    Task ClearAsync(int cartId);
    Task<Cart> GetWithItemsAsync(int userId);
}
