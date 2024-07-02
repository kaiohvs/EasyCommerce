using EasyCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyCommerce.Data
{
    public class BD_Context : DbContext
    {
        public BD_Context(DbContextOptions<BD_Context> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
