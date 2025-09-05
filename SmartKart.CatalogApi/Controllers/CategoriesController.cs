using Microsoft.AspNetCore.Mvc;
using SmartKart.CatalogApi.Domain.Exceptions;
using SmartKart.CatalogApi.Application.Dto;
using SmartKart.CatalogApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SmartKart.CatalogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize("AdminOnly")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="categoryDto">A DTO containing the category details.</param>
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            try
            {
                await _categoryService.CreateCategory(categoryDto, CancellationToken.None);
                // Returns a 201 Created response
                return CreatedAtAction(nameof(GetCategory), new { id = categoryDto.Id }, categoryDto);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="categoryDto">A DTO containing the category details.</param>
        [HttpPost("batch")]
        public async Task<IActionResult> CreateCategory([FromBody] List<CategoryDto> categoryDtos)
        {
            try
            {
                await _categoryService.CreateCategories(categoryDtos, CancellationToken.None);
                // Return a 201 Created response with the list of created brands
                return CreatedAtAction(nameof(GetCategory), new { ids = string.Join(",", categoryDtos.Select(b => b.Id)) }, categoryDtos);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Gets a category by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories()
        {
            var category =  await _categoryService.GetAllAsync();
            if (category == null)
            {
                return NotFound();
            }
            return category.ToList();
        }

        /// <summary>
        /// Gets a category by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return category;
        }
    }
}
