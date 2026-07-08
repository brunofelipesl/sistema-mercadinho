using Microsoft.EntityFrameworkCore;
using Source.Domain.Entitites;
using Source.Domain.Interfaces.Repositories;
using Source.Infrastructure.Persistence.Context;

namespace Source.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SQLDBContext _context;
        public CategoryRepository(SQLDBContext context)
        {
            _context = context;
        }
        public async Task<Category> AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetByCodeAsync(string code)
        {
            return await _context.Categories.AsNoTracking().Where(c => c.code == code).ToListAsync();
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
