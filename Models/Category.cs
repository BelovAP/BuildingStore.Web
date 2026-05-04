using System.ComponentModel.DataAnnotations;

namespace BuildingStore.Web.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Название категории обязательно")]
    [StringLength(50, ErrorMessage = "Максимальная длина названия — 50 символов")]
    public string Name { get; set; } = string.Empty;

    
    public List<BuildingMaterial> Materials { get; set; } = new();
}
