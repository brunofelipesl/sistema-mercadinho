using Source.Domain.Entitites;

namespace Source.Domain.Interfaces.Repositories
{
    public interface ISupplierRepository
    {
        public Task<IEnumerable<Supplier>> GetByCodeAsync(string code);
        public Task<IEnumerable<Supplier>> GetAllAsync();
        public Task<Supplier> AddSupplierAsync(Supplier supplier);
        public Task<Supplier> UpdateSupplierAsync(Supplier supplier);
        public Task DeleteSupplierAsync(Supplier supplier);
    }
}