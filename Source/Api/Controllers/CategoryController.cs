using Microsoft.AspNetCore.Mvc;
using Source.Domain.Entitites;
using Source.Domain.Interfaces.Services;

namespace Source.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            return Ok(categories);
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
            return Ok(category);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            var validationResult = _categoryService.ValidateCategory(category);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _categoryService.AddCategoryAsync(category);

            return CreatedAtAction(nameof(GetCategoryByCode), new { code = category.code }, category);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
            var validationResult = _categoryService.ValidateCategory(category);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _categoryService.UpdateCategoryAsync(category);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteCategory([FromBody] string categoryCode)
        {
            if (string.IsNullOrWhiteSpace(categoryCode))
                return BadRequest("[Exclusão de categoria] - O código da categoria não pode ser nulo ou vazio.");

            var category = await _categoryService.GetByCodeAsync(categoryCode);
            if (category == null)
                return NotFound("[Exclusão de categoria] - A categoria não foi encontrada.");

            await _categoryService.DeleteCategoryAsync(category);
            return NoContent();
        }

    }
}