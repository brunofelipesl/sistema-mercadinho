using Source.Domain.Entitites;
using Source.Domain.Interfaces.Services;

namespace Source.Application.Services
{
    public class ProductService : IProductService
    {
        public Task AddProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}