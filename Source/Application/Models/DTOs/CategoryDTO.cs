using Source.Domain.Entitites;

namespace Source.Application.Models.DTOs
{
    public class CategoryDTO
    {
        public required string Code { get; set; }
        public required string Description { get; set; }

        public static implicit operator CategoryDTO(Category category)
        {
            return new CategoryDTO
            {
                Code = category.code,
                Description = category.description
            };
        }
    }
}