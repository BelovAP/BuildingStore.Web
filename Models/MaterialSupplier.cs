using System.ComponentModel.DataAnnotations;

namespace BuildingStore.Web.Models;

public class MaterialSupplier
{
   
    public int MaterialId { get; set; }
    public BuildingMaterial? Material { get; set; }

    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }     

    [Range(0.01, 1000000, ErrorMessage = "Цена поставки должна быть больше 0")]
    public decimal SupplyPrice { get; set; }     
}
