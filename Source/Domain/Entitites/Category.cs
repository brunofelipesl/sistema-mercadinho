namespace Source.Domain.Entitites
{
    public class Category
    {
        public string code { get; private set; }
        public string description { get; private set; }

        public Category(string code, string description)
        {
            this.code = code;
            this.description = description;
        }
    }
}