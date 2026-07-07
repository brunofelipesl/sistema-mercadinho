using Source.Domain.Entitites;
using Source.Domain.Interfaces.Services;

namespace Source.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierService _supplierService;

        public SupplierService(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        public Task AddSupplierAsync(Supplier supplier)
        {
            if (supplier == null)
            {
                throw new ArgumentNullException("[Cadastro de fornecedor] - O objeto de fornecedor não pode ser nulo.");
            }
            return _supplierService.AddSupplierAsync(supplier);
        }

        public Task DeleteSupplierAsync(Supplier supplier)
        {
            if (supplier == null)
                throw new ArgumentNullException("[Exclusão de fornecedor] - O objeto de fornecedor não pode ser nulo.");
            Supplier supplierInDatabase = _supplierService.GetByCodeAsync(supplier.code).Result;
            if (supplierInDatabase == null)
                throw new InvalidOperationException("[Exclusão de fornecedor] - O fornecedor não foi encontrado.");

            return _supplierService.DeleteSupplierAsync(supplier);
        }

        public Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return _supplierService.GetAllAsync();
        }

        public Task<Supplier> GetByCodeAsync(string code)
        {
            return _supplierService.GetByCodeAsync(code)
            ?? throw new InvalidOperationException("[Consulta de fornecedor] - O fornecedor não foi encontrado.");
        }

        public Task UpdateSupplierAsync(Supplier supplier)
        {
            if (supplier == null)
                throw new ArgumentNullException("[Atualização de fornecedor] - O objeto de fornecedor não pode ser nulo.");
            Supplier supplierInDatabase = _supplierService.GetByCodeAsync(supplier.code).Result;
            if (supplierInDatabase == null)
                throw new InvalidOperationException("[Atualização de fornecedor] - O fornecedor não foi encontrado.");
            return _supplierService.UpdateSupplierAsync(supplier);
        }
    }
}