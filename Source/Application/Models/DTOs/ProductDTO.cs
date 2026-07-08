using Source.Domain.Entitites;

namespace Source.Application.Models.DTOs
{
    public class ProductDTO
    {
        public required string Code { get; set; }
        public required string Description { get; set; }
        public required decimal SellingPrice { get; set; }
        public required DateTime ExpirationDate { get; set; }
        public required decimal ReplacementCost { get; set; }
        public required int StockQuantity { get; set; }
        public required List<CategoryDTO> Categories { get; set; }
        public required List<SupplierDTO> Suppliers { get; set; }

        public static implicit operator ProductDTO(Product product)
        {
            return new ProductDTO
            {
                Code = product.code,
                Description = product.description,
                SellingPrice = product.sellingPrice,
                ExpirationDate = product.expirationDate,
                ReplacementCost = product.replacementCost,
                StockQuantity = product.stockQuantity,
                Categories = product.categories.Select(c => (CategoryDTO)c).ToList(),
                Suppliers = product.suppliers.Select(s => (SupplierDTO)s).ToList()
            };
        }
    }
}