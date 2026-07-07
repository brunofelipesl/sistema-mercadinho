using Source.Domain.Entitites;
using Source.Domain.Interfaces.Repositories;
using Source.Domain.Interfaces.Services;

namespace Source.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task AddCategoryAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("[Criação de categoria] - O objeto de categoria não pode ser nulo.");

            return _categoryRepository.AddCategoryAsync(category);
        }

        public Task DeleteCategoryAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("[Exclusão de categoria] - O objeto de categoria não pode ser nulo.");

            var categoryInDatabase = _categoryRepository.GetByCodeAsync(category.code).Result;

            if (categoryInDatabase == null)
                throw new InvalidOperationException("[Exclusão de categoria] - A categoria não foi encontrada.");

            return _categoryRepository.DeleteCategoryAsync(category);
        }

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return _categoryRepository.GetAllAsync();
        }

        public Task<Category> GetByCodeAsync(string code)
        {
            return _categoryRepository.GetByCodeAsync(code)
            ?? throw new InvalidOperationException("[Consulta de categoria] - A categoria não foi encontrada.");
        }


        public Task UpdateCategoryAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("[Atualização de categoria] - O objeto de categoria não pode ser nulo.");

            var categoryInDatabase = _categoryRepository.GetByCodeAsync(category.code).Result;

            if (categoryInDatabase == null)
                throw new InvalidOperationException("[Atualização de categoria] - A categoria não foi encontrada.");

            return _categoryRepository.UpdateCategoryAsync(category);
        }
    }
}