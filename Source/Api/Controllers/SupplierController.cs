using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Source.Application.Models.Common;
using Source.Domain.Entitites;
using Source.Domain.Interfaces.Services;

namespace Source.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
            Response<IEnumerable<Supplier>> response = new Response<IEnumerable<Supplier>>
            {
                Success = true,
                Data = suppliers,
                Errors = new List<string>()
            };
            return Ok(response);
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
            Response<Supplier> response = new Response<Supplier>
            {
                Success = true,
                Data = supplier,
                Errors = new List<string>()
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateSupplier([FromBody] Supplier supplier)
        {


            var validationResult = _supplierService.ValidateSupplier(supplier);
            Response<Supplier> response = new Response<Supplier>()
            {
                Success = validationResult.IsValid,
            };

            if (!validationResult.IsValid)
            {
                response.Errors = [.. validationResult.Errors];
                return BadRequest(response);
            }

            supplier = await _supplierService.AddSupplierAsync(supplier);
            response.Data = supplier;
            return Ok(response);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateSupplier([FromBody] Supplier supplier)
        {

            var validationResult = _supplierService.ValidateSupplier(supplier);
            Response<Supplier> response = new Response<Supplier>()
            {
                Success = validationResult.IsValid,
            };
            if (!validationResult.IsValid)
            {
                response.Errors = [.. validationResult.Errors];
                return BadRequest(response);
            }

            var updatedSupplier = await _supplierService.UpdateSupplierAsync(supplier);
            response.Data = updatedSupplier;
            return Ok(response);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteSupplier([FromBody] string supplierCode)
        {
            Response<Supplier> response = new Response<Supplier>();

            if (string.IsNullOrWhiteSpace(supplierCode))
            {
                response.Success = false;
                response.Errors = new List<string> { "[Exclusão de fornecedor] - O código do fornecedor não pode ser nulo ou vazio." };
                return BadRequest(response);
            }

            var supplier = await _supplierService.GetByCodeAsync(supplierCode);
            if (supplier == null)
            {
                response.Success = false;
                response.Errors = new List<string> { "[Exclusão de fornecedor] - O fornecedor não foi encontrado." };
                return NotFound(response);
            }

            await _supplierService.DeleteSupplierAsync(supplier);
            response.Success = true;
            return Ok(response);
        }
    }
}