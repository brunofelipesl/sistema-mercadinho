using Source.Application.Models.DTOs;
using Source.Domain.Entitites;

namespace Source.Application.Utils.Extensions
{
    public static class CategoryExtensions
    {
        public static CategoryDTO ToDTO(this Category category)
        {
            return new CategoryDTO
            {
                Code = category.code,
                Description = category.description
            };
        }

        public static Category ToEntity(this CategoryDTO categoryDTO)
        {
            return new Category(
                categoryDTO.Code,
                categoryDTO.Description
            );
        }

        public static IEnumerable<CategoryDTO> ToDTO(this IEnumerable<Category> categories)
        {
            return categories.Select(c => c.ToDTO());
        }

        public static List<Category> ToEntity(this IEnumerable<CategoryDTO> categoryDTOs)
        {
            return categoryDTOs.Select(c => c.ToEntity()).ToList();
        }
    }
}
