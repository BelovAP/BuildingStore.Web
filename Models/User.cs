using System.ComponentModel.DataAnnotations;

namespace BuildingStore.Web.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Логин обязателен")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Логин от 3 до 50 символов")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный формат Email")]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Пароль обязателен")]
    [StringLength(256)]
    public string PasswordHash { get; set; } = string.Empty;

    [StringLength(50)]
    public string? FirstName { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
