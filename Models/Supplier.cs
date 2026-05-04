namespace BuildingStore.Web.Models;
public class Supplier { public int Id { get; set; } public string CompanyName { get; set; } = string.Empty; public List<MaterialSupplier> Materials { get; set; } = new(); }
