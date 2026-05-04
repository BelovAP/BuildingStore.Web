using System.ComponentModel.DataAnnotations;
using BuildingStore.Web.Models;

namespace BuildingStore.Web.Models;

public class CartItem
{
    [Key]
    public int Id { get; set; }

    public int CartId { get; set; }
    public Cart? Cart { get; set; }

    public int MaterialId { get; set; }
    public BuildingMaterial? Material { get; set; }

    [Range(1, 999)]
    public int Quantity { get; set; } = 1;
}