using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingStore.Web.Models;

public class BuildingMaterial
{
    [Key] public int Id { get; set; }

    [Required, StringLength(150)] public string Name { get; set; } = string.Empty;
    [Required, Range(0.01, 1000000)] public decimal Price { get; set; }
    [Range(0, int.MaxValue)] public int Stock { get; set; }

    // 1:N связь
    [Required] public int CategoryId { get; set; }
    public Category? Category { get; set; }

    // 1:1 связь
    public TechnicalSpec? Spec { get; set; }

    // N:M связь
    public List<MaterialSupplier> Suppliers { get; set; } = new();
}

