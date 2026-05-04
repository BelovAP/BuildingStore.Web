using BuildingStore.Web.Models;

namespace BuildingStore.Web.Services;

public interface ICartService
{
    Task<Cart> GetCartAsync(int userId);
    Task AddToCartAsync(int userId, int materialId, int quantity = 1);
    Task RemoveFromCartAsync(int userId, int cartItemId);
    Task UpdateQuantityAsync(int userId, int cartItemId, int quantity);
    Task ClearCartAsync(int userId);
    decimal GetTotalPrice(Cart cart);
}
