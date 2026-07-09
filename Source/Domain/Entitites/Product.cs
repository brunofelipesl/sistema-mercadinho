namespace Source.Domain.Entitites
{
    public class Product
    {
        public string code { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public List<Category> categories { get; set; } = new List<Category>();
        public List<Supplier> suppliers { get; set; } = new List<Supplier>();
        public decimal sellingPrice { get; set; }
        public decimal replacementCost { get; set; }
        public DateTime expirationDate { get; set; }
        public int stockQuantity { get; set; }

        public Product()
        {
        }

        public Product(string code, string description, decimal sellingPrice, decimal replacementCost, DateTime expirationDate, int stockQuantity)
        {
            this.code = code;
            this.description = description;
            this.sellingPrice = sellingPrice;
            this.replacementCost = replacementCost;
            this.expirationDate = expirationDate;
            this.stockQuantity = stockQuantity;
        }
    }
}