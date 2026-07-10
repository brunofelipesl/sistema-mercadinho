namespace Source.Application.Models.DTOs
{
    public class CategoryDTO
    {
        public string Code { get; set; } = string.Empty;
        public required string Description { get; set; }
    }
}