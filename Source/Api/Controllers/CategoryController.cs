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
            Response<IEnumerable<CategoryDTO>> response = new Response<IEnumerable<CategoryDTO>>
            {
                Success = true,
                Data = categories.ToDTO().OrderBy(c => c.Description).ToList(),
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
            Response<CategoryDTO> response = new Response<CategoryDTO>
            {
                Success = true,
                Data = category.ToDTO(),
                Errors = new List<string>()
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            var category = categoryDTO.ToEntity();
            var validationResult = _categoryService.ValidateCategory(category);
            Response<CategoryDTO> response = new Response<CategoryDTO>()
            {
                Success = validationResult.IsValid
            };
            if (!validationResult.IsValid)
            {
                response.Errors = [.. validationResult.Errors];
                return BadRequest(response);
            }

            var createdCategory = await _categoryService.AddCategoryAsync(category);
            response.Data = createdCategory.ToDTO();
            return Ok(response);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDTO categoryDTO)
        {
            var category = categoryDTO.ToEntity();
            var validationResult = _categoryService.ValidateCategory(category);
            Response<CategoryDTO> response = new Response<CategoryDTO>()
            {
                Success = validationResult.IsValid
            };
            if (!validationResult.IsValid)
            {
                response.Errors = [.. validationResult.Errors];
                return BadRequest(response);
            }

            var updatedCategory = await _categoryService.UpdateCategoryAsync(category);
            response.Data = updatedCategory.ToDTO();
            return Ok(response);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteCategory([FromBody] string categoryCode)
        {
            Response<CategoryDTO> response = new Response<CategoryDTO>();
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