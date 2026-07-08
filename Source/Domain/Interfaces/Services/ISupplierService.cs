using Source.Application.Models.Common;
using Source.Domain.Entitites;

namespace Source.Domain.Interfaces.Services
{
    public interface ISupplierService
    {
        public Task<Supplier> GetByCodeAsync(string code);
        public Task<IEnumerable<Supplier>> GetAllAsync();
        public Task<Supplier> AddSupplierAsync(Supplier supplier);
        public Task<Supplier> UpdateSupplierAsync(Supplier supplier);
        public Task DeleteSupplierAsync(Supplier supplier);
        public ValidationResult ValidateSupplier(Supplier supplier);
    }
}