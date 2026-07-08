using Source.Domain.Entitites;

namespace Source.Application.Models.DTOs
{
    public class SupplierDTO
    {
        public required string Code { get; set; }
        public required string Name { get; set; }

        public static implicit operator SupplierDTO(Supplier supplier)
        {
            return new SupplierDTO
            {
                Code = supplier.code,
                Name = supplier.name
            };
        }
    }
}