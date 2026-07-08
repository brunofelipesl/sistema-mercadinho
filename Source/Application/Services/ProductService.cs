using Source.Application.Models.Common;
using Source.Application.Utils;
using Source.Domain.Entitites;
using Source.Domain.Interfaces.Repositories;
using Source.Domain.Interfaces.Services;

namespace Source.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Product> AddProductAsync(Product product)
        {
            Product productToBeAdded = new Product(
                EntityUniqueCodeGenerator.GenerateUniqueCode<Product>(),
                product.description,
                product.categories,
                product.suppliers,
                product.sellingPrice,
                product.replacementCost,
                product.expirationDate,
                product.stockQuantity
            );

            return _productRepository.AddProductAsync(productToBeAdded);
        }

        public Task DeleteProductAsync(Product product)
        {
            var productInDatabase = _productRepository.GetByCodeAsync(product.code).Result;

            if (productInDatabase == null)
                throw new InvalidOperationException("[Exclusão de produto] - O produto não foi encontrado.");

            return _productRepository.DeleteProductAsync(product);
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return _productRepository.GetAllAsync();
        }

        public Task<Product> GetByCodeAsync(string code)
        {
            return _productRepository.GetByCodeAsync(code)
                ?? throw new InvalidOperationException("[Consulta de produto] - O produto não foi encontrado.");
        }

        public Task<Product> UpdateProductAsync(Product product)
        {
            var productInDatabase = _productRepository.GetByCodeAsync(product.code).Result;

            if (productInDatabase == null)
                throw new InvalidOperationException("[Atualização de produto] - O produto não foi encontrado.");

            return _productRepository.UpdateProductAsync(product);
        }

        public ValidationResult ValidateProduct(Product product)
        {
            var validationResult = new ValidationResult();

            if (product == null)
            {
                validationResult.AddError("[Validação de produto] - O objeto de produto não pode ser nulo.");
                return validationResult;
            }

            if (string.IsNullOrWhiteSpace(product.description))
                validationResult.AddError("[Validação de produto] - A descrição do produto não pode ser vazia.");

            return validationResult;
        }
    }
}