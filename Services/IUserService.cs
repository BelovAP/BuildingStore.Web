using BuildingStore.Web.Models;

namespace BuildingStore.Web.Services;

public interface IUserService
{
    Task<bool> RegisterAsync(RegisterDto dto);
    Task<User?> LoginAsync(LoginDto dto);
    Task<User?> GetByIdAsync(int id);
}
