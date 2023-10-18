using InvenTrackPro.API.Models;
using Microsoft.EntityFrameworkCore;

namespace InvenTrackPro.API.Data
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductVariation> ProductVariations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<ProductVariation>().ToTable("ProductVariations");

            base.OnModelCreating(modelBuilder);
        }
    }
}
