using EasyCommerce.Data;
using EasyCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyCommerce.Interfaces
{
    public class ProductsRepository : IProductsRepository
    { 
        private readonly BD_Context _context;        

        public ProductsRepository(BD_Context context)
        {
            _context = context;
           
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var list = await _context.Products
                .Include(p=> p.ProductCategory)
                .ToListAsync();

            return list;
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(Product entity)
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Products.FindAsync(id);
            if (entity != null)
            {
                _context.Products.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
