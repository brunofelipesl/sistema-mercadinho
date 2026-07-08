using Microsoft.AspNetCore.Mvc;
using Source.Application.Models.Common;
using Source.Domain.Entitites;
using Source.Domain.Interfaces.Services;

namespace Source.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            Response<IEnumerable<Product>> response = new Response<IEnumerable<Product>>
            {
                Success = true,
                Data = products,
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

            Response<Product> response = new Response<Product>
            {
                Success = true,
                Data = product,
                Errors = new List<string>()
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            var validationResult = _productService.ValidateProduct(product);
            Response<Product> response = new Response<Product>
            {
                Success = validationResult.IsValid
            };

            if (!validationResult.IsValid)
            {
                response.Errors = [.. validationResult.Errors];
                return BadRequest(response);
            }

            product = await _productService.AddProductAsync(product);
            response.Data = product;
            return Ok(response);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            var validationResult = _productService.ValidateProduct(product);
            Response<Product> response = new Response<Product>
            {
                Success = validationResult.IsValid
            };

            if (!validationResult.IsValid)
            {
                response.Errors = [.. validationResult.Errors];
                return BadRequest(response);
            }

            var updatedProduct = await _productService.UpdateProductAsync(product);
            response.Data = updatedProduct;
            return Ok(response);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteProduct([FromBody] string productCode)
        {
            Response<Product> response = new Response<Product>();

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
