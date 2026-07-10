using Microsoft.EntityFrameworkCore;
using Source.Domain.Entitites;
using Source.Domain.Interfaces.Repositories;
using Source.Infrastructure.Persistence.Context;

namespace Source.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SQLDBContext _context;
        public ProductRepository(SQLDBContext context)
        {
            _context = context;
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            product.categories = await ResolveCategoriesAsync(product.categories);
            product.suppliers = await ResolveSuppliersAsync(product.suppliers);

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .Include(p => p.categories)
                .Include(p => p.suppliers)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCodeAsync(string code)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(p => p.categories)
                .Include(p => p.suppliers)
                .Where(p => p.code == code)
                .ToListAsync();
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var productInDatabase = await _context.Products
                .Include(p => p.categories)
                .Include(p => p.suppliers)
                .FirstOrDefaultAsync(p => p.code == product.code);

            if (productInDatabase == null)
                throw new InvalidOperationException("[Atualizacao de produto] - O produto nao foi encontrado.");

            productInDatabase.description = product.description;
            productInDatabase.sellingPrice = product.sellingPrice;
            productInDatabase.replacementCost = product.replacementCost;
            productInDatabase.expirationDate = product.expirationDate;
            productInDatabase.stockQuantity = product.stockQuantity;

            productInDatabase.categories = await ResolveCategoriesAsync(product.categories);
            productInDatabase.suppliers = await ResolveSuppliersAsync(product.suppliers);

            await _context.SaveChangesAsync();
            return productInDatabase;
        }

        private async Task<List<Category>> ResolveCategoriesAsync(IEnumerable<Category> categories)
        {
            var categoryCodes = categories
                .Select(c => c.code)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct()
                .ToList();

            var categoriesInDatabase = await _context.Categories
                .Where(c => categoryCodes.Contains(c.code))
                .ToListAsync();

            var missingCodes = categoryCodes
                .Except(categoriesInDatabase.Select(c => c.code))
                .ToList();

            if (missingCodes.Any())
                throw new InvalidOperationException($"[Cadastro de produto] - Categorias nao encontradas: {string.Join(", ", missingCodes)}");

            return categoriesInDatabase;
        }

        private async Task<List<Supplier>> ResolveSuppliersAsync(IEnumerable<Supplier> suppliers)
        {
            var supplierCodes = suppliers
                .Select(s => s.code)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct()
                .ToList();

            var suppliersInDatabase = await _context.Suppliers
                .Where(s => supplierCodes.Contains(s.code))
                .ToListAsync();

            var missingCodes = supplierCodes
                .Except(suppliersInDatabase.Select(s => s.code))
                .ToList();

            if (missingCodes.Any())
                throw new InvalidOperationException($"[Cadastro de produto] - Fornecedores nao encontrados: {string.Join(", ", missingCodes)}");

            return suppliersInDatabase;
        }
    }
}