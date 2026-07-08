using Source.Application.Models.Common;
using Source.Domain.Entitites;

namespace Source.Domain.Interfaces.Services
{
    public interface IProductService
    {
        public Task<Product> GetByCodeAsync(string code);
        public Task<IEnumerable<Product>> GetAllAsync();
        public Task<Product> AddProductAsync(Product product);
        public Task<Product> UpdateProductAsync(Product product);
        public Task DeleteProductAsync(Product product);
        public ValidationResult ValidateProduct(Product product);
    }
}