using Source.Domain.Entitites;

namespace Source.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetByCodeAsync(string code);
        public Task<IEnumerable<Category>> GetAllAsync();
        public Task<Category> AddCategoryAsync(Category category);
        public Task<Category> UpdateCategoryAsync(Category category);
        public Task DeleteCategoryAsync(Category category);
    }
}