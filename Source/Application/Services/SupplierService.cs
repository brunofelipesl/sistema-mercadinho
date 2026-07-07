using Source.Application.Common;
using Source.Application.Utils;
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
            Supplier supplierToBeAdded = new Supplier(
                EntityUniqueCodeGenerator.GenerateUniqueCode<Supplier>(),
                supplier.name
            );
            return _supplierService.AddSupplierAsync(supplierToBeAdded);
        }

        public Task DeleteSupplierAsync(Supplier supplier)
        {
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
            Supplier supplierInDatabase = _supplierService.GetByCodeAsync(supplier.code).Result;
            if (supplierInDatabase == null)
                throw new InvalidOperationException("[Atualização de fornecedor] - O fornecedor não foi encontrado.");
            return _supplierService.UpdateSupplierAsync(supplier);
        }

        public ValidationResult ValidateSupplier(Supplier supplier)
        {
            var validationResult = new ValidationResult();

            if (supplier == null)
            {
                validationResult.AddError("[Validação de fornecedor] - O objeto de fornecedor não pode ser nulo.");
                return validationResult;
            }

            if (string.IsNullOrWhiteSpace(supplier.name))
                validationResult.AddError("[Validação de fornecedor] - O nome do fornecedor não pode ser vazio.");

            return validationResult;
        }
    }
}