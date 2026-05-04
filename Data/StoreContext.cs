using BuildingStore.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildingStore.Web.Data;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<BuildingMaterial> Materials { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<TechnicalSpec> TechnicalSpecs { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);

        // 1. Настройка Users
        mb.Entity<User>().ToTable("Users");
        mb.Entity<User>().HasIndex(u => u.Username).IsUnique();

        // 2. Настройка Materials (Тип decimal для SQLite)
        mb.Entity<BuildingMaterial>().ToTable("Materials");
        mb.Entity<BuildingMaterial>()
          .Property(m => m.Price)
          .HasColumnType("decimal(10,2)"); // Важно для SQLite

        // 3. Связь 1:N (Category -> Materials)
        mb.Entity<Category>().HasMany(c => c.Materials)
            .WithOne(m => m.Category)
            .HasForeignKey(m => m.CategoryId)
            .OnDelete(DeleteBehavior.Restrict); // Чтобы не удалять категорию с товарами

        // 4. Связь 1:1 (Material <-> Spec)
        mb.Entity<TechnicalSpec>().HasOne(t => t.Material)
            .WithOne(m => m.Spec)
            .HasForeignKey<TechnicalSpec>(t => t.MaterialId)
            .OnDelete(DeleteBehavior.Cascade);

        // 5. Связь N:M (Material <-> Supplier через MaterialSupplier)
        mb.Entity<MaterialSupplier>().HasKey(ms => new { ms.MaterialId, ms.SupplierId });

        mb.Entity<MaterialSupplier>().HasOne(ms => ms.Material)
            .WithMany(m => m.Suppliers)
            .HasForeignKey(ms => ms.MaterialId);

        mb.Entity<MaterialSupplier>().HasOne(ms => ms.Supplier)
            .WithMany(s => s.Materials)
            .HasForeignKey(ms => ms.SupplierId);

        // 1:1 User ↔ Cart
        mb.Entity<Cart>().HasOne(c => c.User).WithOne().HasForeignKey<Cart>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // 1:N Cart → CartItems
        mb.Entity<CartItem>().HasOne(ci => ci.Cart).WithMany(c => c.Items)
            .HasForeignKey(ci => ci.CartId).OnDelete(DeleteBehavior.Cascade);

        // N:1 CartItem → Material
        mb.Entity<CartItem>().HasOne(ci => ci.Material).WithMany()
            .HasForeignKey(ci => ci.MaterialId).OnDelete(DeleteBehavior.Restrict);
    }
}
