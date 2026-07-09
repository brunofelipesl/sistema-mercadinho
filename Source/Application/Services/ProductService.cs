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
                product.sellingPrice,
                product.replacementCost,
                product.expirationDate,
                product.stockQuantity
            )
            {
                categories = product.categories,
                suppliers = product.suppliers
            };

            return _productRepository.AddProductAsync(productToBeAdded);
        }

        public Task DeleteProductAsync(Product product)
        {
            var productInDatabase = _productRepository.GetByCodeAsync(product.code).Result;

            if (productInDatabase == null || !productInDatabase.Any())
                throw new InvalidOperationException("[Exclusão de produto] - O produto não foi encontrado.");

            return _productRepository.DeleteProductAsync(product);
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return _productRepository.GetAllAsync();
        }

        public Task<Product> GetByCodeAsync(string code)
        {
            var products = _productRepository.GetByCodeAsync(code).Result;
            if (products == null || !products.Any())
                throw new InvalidOperationException("[Consulta de produto] - O produto não foi encontrado.");

            return Task.FromResult(products.First());
        }

        public Task<Product> UpdateProductAsync(Product product)
        {
            var productInDatabase = _productRepository.GetByCodeAsync(product.code).Result;

            if (productInDatabase == null || !productInDatabase.Any())
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

            if (product.categories == null || !product.categories.Any())
                validationResult.AddError("[Validação de produto] - O produto deve ter pelo menos uma categoria associada.");

            if (product.suppliers == null || !product.suppliers.Any())
                validationResult.AddError("[Validação de produto] - O produto deve ter pelo menos um fornecedor associado.");

            if (product.sellingPrice <= 0)
                validationResult.AddError("[Validação de produto] - O preço de venda do produto deve ser maior que zero.");

            if (product.replacementCost <= 0)
                validationResult.AddError("[Validação de produto] - O custo de reposição do produto deve ser maior que zero.");

            if (product.expirationDate <= DateTime.Now)
                validationResult.AddError("[Validação de produto] - A data de validade do produto deve ser uma data futura.");

            if (product.stockQuantity < 0)
                validationResult.AddError("[Validação de produto] - A quantidade em estoque do produto não pode ser negativa.");

            return validationResult;
        }
    }
}