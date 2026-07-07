using Microsoft.AspNetCore.Mvc;
using Source.Domain.Entitites;
using Source.Domain.Interfaces.Services;

namespace Source.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var suppliers = await _supplierService.GetAllAsync();
            return Ok(suppliers);
        }

        [HttpGet]
        [Route("get-by-code/{code}")]
        public async Task<IActionResult> GetSupplierByCode(string code)
        {
            var supplier = await _supplierService.GetByCodeAsync(code);
            if (supplier == null)
            {
                return NotFound();
            }
            return Ok(supplier);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateSupplier([FromBody] Supplier supplier)
        {


            var validationResult = _supplierService.ValidateSupplier(supplier);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _supplierService.AddSupplierAsync(supplier);
            return CreatedAtAction(nameof(GetSupplierByCode), new { code = supplier.code }, supplier);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateSupplier([FromBody] Supplier supplier)
        {

            var validationResult = _supplierService.ValidateSupplier(supplier);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _supplierService.UpdateSupplierAsync(supplier);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteSupplier([FromBody] string supplierCode)
        {
            if (string.IsNullOrWhiteSpace(supplierCode))
                return BadRequest("[Exclusão de fornecedor] - O código do fornecedor não pode ser nulo ou vazio.");

            var supplier = await _supplierService.GetByCodeAsync(supplierCode);
            if (supplier == null)
                return NotFound("[Exclusão de fornecedor] - O fornecedor não foi encontrado.");

            await _supplierService.DeleteSupplierAsync(supplier);
            return NoContent();
        }
    }
}