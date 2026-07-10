using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Source.Application.Models.Common;
using Source.Application.Models.DTOs;
using Source.Application.Utils.Extensions;
using Source.Domain.Interfaces.Services;

namespace Source.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllAsync();
            Response<IEnumerable<ProductDTO>> response = new Response<IEnumerable<ProductDTO>>
            {
                Success = true,
                Data = products.ToDTO(),
                Errors = new List<string>()
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("get-by-code/{code}")]
        public async Task<IActionResult> GetProductByCode(string code)
        {
            var product = await _productService.GetByCodeAsync(code);
            if (product == null)
            {
                return NotFound();
            }

            Response<ProductDTO> response = new Response<ProductDTO>
            {
                Success = true,
                Data = product.ToDTO(),
                Errors = new List<string>()
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO productDTO)
        {
            var product = productDTO.ToEntity();
            var validationResult = _productService.ValidateProduct(product);
            Response<ProductDTO> response = new Response<ProductDTO>
            {
                Success = validationResult.IsValid
            };

            if (!validationResult.IsValid)
            {
                response.Errors = [.. validationResult.Errors];
                return BadRequest(response);
            }

            var createdProduct = await _productService.AddProductAsync(product);
            response.Data = createdProduct.ToDTO();
            return Ok(response);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO productDTO)
        {
            var product = productDTO.ToEntity();
            var validationResult = _productService.ValidateProduct(product);
            Response<ProductDTO> response = new Response<ProductDTO>
            {
                Success = validationResult.IsValid
            };

            if (!validationResult.IsValid)
            {
                response.Errors = [.. validationResult.Errors];
                return BadRequest(response);
            }

            var updatedProduct = await _productService.UpdateProductAsync(product);
            response.Data = updatedProduct.ToDTO();
            return Ok(response);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteProduct([FromBody] string productCode)
        {
            Response<ProductDTO> response = new Response<ProductDTO>();

            if (string.IsNullOrWhiteSpace(productCode))
            {
                response.Success = false;
                response.Errors = new List<string> { "[Exclusão de produto] - O código do produto não pode ser nulo ou vazio." };
                return BadRequest(response);
            }

            var product = await _productService.GetByCodeAsync(productCode);
            if (product == null)
            {
                response.Success = false;
                response.Errors = new List<string> { "[Exclusão de produto] - O produto não foi encontrado." };
                return NotFound(response);
            }

            await _productService.DeleteProductAsync(product);
            response.Success = true;
            return Ok(response);
        }
    }
}
