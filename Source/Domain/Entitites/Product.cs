namespace Source.Domain.Entitites
{
    public class Product
    {
        public string code { get; private set; }
        public string description { get; private set; }
        public List<Category> categories { get; private set; }
        public List<Supplier> suppliers { get; private set; }
        public decimal sellingPrice { get; private set; }
        public decimal replacementCost { get; private set; }
        public DateTime expirationDate { get; private set; }
        public int stockQuantity { get; private set; }

        public Product(string code, string description, List<Category> categories, List<Supplier> suppliers, decimal sellingPrice, decimal replacementCost, DateTime expirationDate, int stockQuantity)
        {
            this.code = code;
            this.description = description;
            this.categories = categories;
            this.suppliers = suppliers;
            this.sellingPrice = sellingPrice;
            this.replacementCost = replacementCost;
            this.expirationDate = expirationDate;
            this.stockQuantity = stockQuantity;
        }

    }
}