using Source.Domain.Entitites;

namespace Source.Domain.Interfaces.Services
{
    public interface IProductService
    {
        public Task<Product> GetByCodeAsync(string code);
        public Task<IEnumerable<Product>> GetAllAsync();
        public Task AddProductAsync(Product product);
        public Task UpdateProductAsync(Product product);
        public Task DeleteProductAsync(Product product);
    }
}