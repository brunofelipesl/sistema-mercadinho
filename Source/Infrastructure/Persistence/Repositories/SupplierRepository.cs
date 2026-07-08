using Microsoft.EntityFrameworkCore;
using Source.Domain.Entitites;
using Source.Domain.Interfaces.Repositories;
using Source.Infrastructure.Persistence.Context;

namespace Source.Infrastructure.Persistence.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly SQLDBContext _context;
        public SupplierRepository(SQLDBContext context)
        {
            _context = context;
        }
        public async Task<Supplier> AddSupplierAsync(Supplier supplier)
        {
            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task DeleteSupplierAsync(Supplier supplier)
        {
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _context.Suppliers.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Supplier>> GetByCodeAsync(string code)
        {
            return await _context.Suppliers.AsNoTracking().Where(s => s.code == code).ToListAsync();
        }

        public async Task<Supplier> UpdateSupplierAsync(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }
    }
}
