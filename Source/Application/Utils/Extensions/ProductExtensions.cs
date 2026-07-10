using Source.Application.Models.DTOs;
using Source.Domain.Entitites;

namespace Source.Application.Utils.Extensions
{
    public static class ProductExtensions
    {
        public static ProductDTO ToDTO(this Product product)
        {
            return new ProductDTO
            {
                Code = product.code,
                Description = product.description,
                SellingPrice = product.sellingPrice,
                ExpirationDate = product.expirationDate,
                ReplacementCost = product.replacementCost,
                StockQuantity = product.stockQuantity,
                Categories = product.categories.ToDTO().ToList(),
                Suppliers = product.suppliers.ToDTO().ToList()
            };
        }

        public static Product ToEntity(this ProductDTO productDTO)
        {
            return new Product
            (
                code: productDTO.Code,
                description: productDTO.Description,
                sellingPrice: productDTO.SellingPrice,
                expirationDate: productDTO.ExpirationDate,
                replacementCost: productDTO.ReplacementCost,
                stockQuantity: productDTO.StockQuantity
            )
            {
                categories = productDTO.Categories.ToEntity(),
                suppliers = productDTO.Suppliers.ToEntity()
            };
        }

        public static IEnumerable<ProductDTO> ToDTO(this IEnumerable<Product> products)
        {
            return products.Select(p => p.ToDTO());
        }

        public static List<Product> ToEntity(this IEnumerable<ProductDTO> productDTOs)
        {
            return productDTOs.Select(p => p.ToEntity()).ToList();
        }
    }
}
