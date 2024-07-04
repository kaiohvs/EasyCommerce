using EasyCommerce.Autentication;
using EasyCommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyCommerce.Data
{
    public class BD_Context : IdentityDbContext<ApplicationUser>
    {
        public BD_Context(DbContextOptions<BD_Context> options) : base(options) { }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductCategory>()
           .HasMany(pc => pc.SubCategories)
           .WithOne(pc => pc.ParentCategory)
           .HasForeignKey(pc => pc.ParentCategoryId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductCategory)
                .WithMany(pc => pc.Products)
                .HasForeignKey(p => p.ProductCategoryId);
        }
    }
}
