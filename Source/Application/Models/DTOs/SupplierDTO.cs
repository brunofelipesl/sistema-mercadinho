namespace Source.Application.Models.DTOs
{
    public class SupplierDTO
    {
        public string Code { get; set; } = string.Empty;
        public required string Name { get; set; }
    }
}