namespace Source.Domain.Entitites
{
    public class Supplier
    {
        public string code { get; private set; }
        public string name { get; private set; }

        public Supplier(string code, string name)
        {
            this.code = code;
            this.name = name;
        }
    }
}