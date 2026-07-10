namespace Source.Application.Models.DTOs
{
    public class ProductDTO
    {
        public string Code { get; set; } = string.Empty;
        public required string Description { get; set; }
        public required decimal SellingPrice { get; set; }
        public required DateTime ExpirationDate { get; set; }
        public required decimal ReplacementCost { get; set; }
        public required int StockQuantity { get; set; }
        public required List<CategoryDTO> Categories { get; set; }
        public required List<SupplierDTO> Suppliers { get; set; }
    }
}