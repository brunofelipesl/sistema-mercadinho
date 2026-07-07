using Source.Application.Common;
using Source.Application.Utils;
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
            Category categoryToBeAdded = new Category(
                 EntityUniqueCodeGenerator.GenerateUniqueCode<Category>(),
                 category.description
             );

            return _categoryRepository.AddCategoryAsync(categoryToBeAdded);
        }

        public Task DeleteCategoryAsync(Category category)
        {
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
            var categoryInDatabase = _categoryRepository.GetByCodeAsync(category.code).Result;

            if (categoryInDatabase == null)
                throw new InvalidOperationException("[Atualização de categoria] - A categoria não foi encontrada.");

            return _categoryRepository.UpdateCategoryAsync(category);
        }

        public ValidationResult ValidateCategory(Category category)
        {
            var validationResult = new ValidationResult();

            if (category == null)
            {
                validationResult.AddError("[Validação de categoria] - O objeto de categoria não pode ser nulo.");
                return validationResult;
            }

            if (string.IsNullOrWhiteSpace(category.description))
                validationResult.AddError("[Validação de categoria] - A descrição da categoria não pode ser vazio.");

            return validationResult;
        }
    }
}