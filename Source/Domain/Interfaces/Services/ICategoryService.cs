using Source.Application.Models.Common;
using Source.Domain.Entitites;

namespace Source.Domain.Interfaces.Services
{
    public interface ICategoryService
    {
        public Task<Category> GetByCodeAsync(string code);
        public Task<IEnumerable<Category>> GetAllAsync();
        public Task<Category> AddCategoryAsync(Category category);
        public Task<Category> UpdateCategoryAsync(Category category);
        public Task DeleteCategoryAsync(Category category);
        public ValidationResult ValidateCategory(Category category);
    }
}