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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            Response<IEnumerable<Category>> response = new Response<IEnumerable<Category>>
            {
                Success = true,
                Data = categories,
                Errors = new List<string>()
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("get-by-code/{code}")]
        public async Task<IActionResult> GetCategoryByCode(string code)
        {
            var category = await _categoryService.GetByCodeAsync(code);
            if (category == null)
            {
                return NotFound();
            }
            Response<Category> response = new Response<Category>
            {
                Success = true,
                Data = category,
                Errors = new List<string>()
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            var validationResult = _categoryService.ValidateCategory(category);
            Response<Category> response = new Response<Category>()
            {
                Success = validationResult.IsValid
            };
            if (!validationResult.IsValid)
            {
                response.Errors = [.. validationResult.Errors];
                return BadRequest(response);
            }

            category = await _categoryService.AddCategoryAsync(category);
            response.Data = category;
            return Ok(response);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
            var validationResult = _categoryService.ValidateCategory(category);
            Response<Category> response = new Response<Category>()
            {
                Success = validationResult.IsValid
            };
            if (!validationResult.IsValid)
            {
                response.Errors = [.. validationResult.Errors];
                return BadRequest(response);
            }

            var updatedCategory = await _categoryService.UpdateCategoryAsync(category);
            response.Data = updatedCategory;
            return Ok(response);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteCategory([FromBody] string categoryCode)
        {
            Response<Category> response = new Response<Category>();
            if (string.IsNullOrWhiteSpace(categoryCode))
            {
                response.Success = false;
                response.Errors = new List<string> { "[Exclusão de categoria] - O código da categoria não pode ser nulo ou vazio." };
                return BadRequest(response);
            }

            var category = await _categoryService.GetByCodeAsync(categoryCode);
            if (category == null)
            {
                response.Success = false;
                response.Errors = new List<string> { "[Exclusão de categoria] - A categoria não foi encontrada." };
                return NotFound(response);
            }

            await _categoryService.DeleteCategoryAsync(category);
            response.Success = true;
            return Ok(response);
        }

    }
}