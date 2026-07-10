using Source.Application.Models.DTOs;
using Source.Domain.Entitites;

namespace Source.Application.Utils.Extensions
{
    public static class SupplierExtensions
    {
        public static SupplierDTO ToDTO(this Supplier supplier)
        {
            return new SupplierDTO
            {
                Code = supplier.code,
                Name = supplier.name
            };
        }

        public static Supplier ToEntity(this SupplierDTO supplierDTO)
        {
            return new Supplier(
                supplierDTO.Code,
                supplierDTO.Name
            );
        }

        public static IEnumerable<SupplierDTO> ToDTO(this IEnumerable<Supplier> suppliers)
        {
            return suppliers.Select(s => s.ToDTO());
        }

        public static List<Supplier> ToEntity(this IEnumerable<SupplierDTO> supplierDTOs)
        {
            return supplierDTOs.Select(s => s.ToEntity()).ToList();
        }
    }
}
