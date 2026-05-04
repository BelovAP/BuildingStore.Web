using System.ComponentModel.DataAnnotations;

namespace BuildingStore.Web.Models;

public class TechnicalSpec
{
    [Key]
    public int Id { get; set; }

    
    [Required]
    public int MaterialId { get; set; }
    public BuildingMaterial? Material { get; set; }

    public string? Weight { get; set; }          // Вес в кг
    public string? Dimensions { get; set; }      // Габариты (например, "600x600x10 мм")
    public bool IsFrostResistant { get; set; }   // Морозостойкость
}
