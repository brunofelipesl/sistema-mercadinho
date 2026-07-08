using Source.Application.Models.Common;
using Source.Application.Utils;
using Source.Domain.Entitites;
using Source.Domain.Interfaces.Repositories;
using Source.Domain.Interfaces.Services;

namespace Source.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public Task<Supplier> AddSupplierAsync(Supplier supplier)
        {
            Supplier supplierToBeAdded = new Supplier(
                EntityUniqueCodeGenerator.GenerateUniqueCode<Supplier>(),
                supplier.name
            );
            return _supplierRepository.AddSupplierAsync(supplierToBeAdded);
        }

        public Task DeleteSupplierAsync(Supplier supplier)
        {
            Supplier supplierInDatabase = _supplierRepository.GetByCodeAsync(supplier.code).Result;
            if (supplierInDatabase == null)
                throw new InvalidOperationException("[Exclusão de fornecedor] - O fornecedor não foi encontrado.");

            return _supplierRepository.DeleteSupplierAsync(supplier);
        }

        public Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return _supplierRepository.GetAllAsync();
        }

        public Task<Supplier> GetByCodeAsync(string code)
        {
            return _supplierRepository.GetByCodeAsync(code)
            ?? throw new InvalidOperationException("[Consulta de fornecedor] - O fornecedor não foi encontrado.");
        }

        public Task<Supplier> UpdateSupplierAsync(Supplier supplier)
        {
            Supplier supplierInDatabase = _supplierRepository.GetByCodeAsync(supplier.code).Result;
            if (supplierInDatabase == null)
                throw new InvalidOperationException("[Atualização de fornecedor] - O fornecedor não foi encontrado.");
            return _supplierRepository.UpdateSupplierAsync(supplier);
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