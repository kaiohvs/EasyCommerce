using EasyCommerce.Autentication;
using EasyCommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyCommerce.Data
{
    public class BD_Context : IdentityDbContext<ApplicationUser>
    {
        public BD_Context(DbContextOptions<BD_Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);            
        }
        public DbSet<Product> Products { get; set; }
    }
}
