using Source.Domain.Entitites;

namespace Source.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        public Task<Category> GetByCodeAsync(string code);
        public Task<IEnumerable<Category>> GetAllAsync();
        public Task AddCategoryAsync(Category category);
        public Task UpdateCategoryAsync(Category category);
        public Task DeleteCategoryAsync(Category category);
    }
}