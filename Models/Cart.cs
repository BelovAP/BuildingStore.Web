using System.ComponentModel.DataAnnotations;
using BuildingStore.Web.Models;

namespace BuildingStore.Web.Models;

public class Cart
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    public User? User { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    
    public List<CartItem> Items { get; set; } = new();
}