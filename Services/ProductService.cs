using EasyCommerce.Interfaces;
using EasyCommerce.Models;

namespace EasyCommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductsRepository _repository;

        public ProductService(IProductsRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Product entity)
        {
            try
            {
                await _repository.AddAsync(entity);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                 await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                var list = await _repository.GetAllAsync();

                if (list == null)
                {
                    return Enumerable.Empty<Product>();
                }
                else
                {
                    return list;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Product entity)
        {
            try
            {
                await _repository.UpdateAsync(entity);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
