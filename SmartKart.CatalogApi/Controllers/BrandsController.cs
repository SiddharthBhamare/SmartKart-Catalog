using Microsoft.AspNetCore.Mvc;
using SmartKart.CatalogApi.Application.Interfaces;
using SmartKart.CatalogApi.Domain.Entities;
using SmartKart.CatalogApi.Domain.Exceptions;
using SmartKart.CatalogApi.Application.Dto;
using SmartKart.CatalogApi.Application.Exceptions;

namespace SmartKart.CatalogApi.Controllers
{
    // The [ApiController] attribute enables API-specific behaviors like
    // automatic model validation and HTTP 400 responses.
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        // Constructor for dependency injection. The DbContext is injected automatically.
        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        /// <summary>
        /// Creates a new brand.
        /// </summary>
        /// <param name="brandName">The name of the brand to create.</param>
        [HttpPost]
        public async Task<ActionResult<Brand>> CreateBrand([FromBody] BrandDto brandDto)
        {
            try
            {
                var brand = await _brandService.CreateBrandAsync(brandDto);
                // Returns a 201 Created response with the brand's details
                return CreatedAtAction(nameof(GetBrand), new { id = brand.Id }, brand);
            }
            catch (DomainException ex)
            {
                // Catches domain-specific exceptions and returns a 400 Bad Request
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Creates multiple new brands from a list of names.
        /// </summary>
        /// <param name="brands">A list of brand names to create.</param>
        [HttpPost("batch")]
        public async Task<ActionResult<IEnumerable<Brand>>> CreateBrands([FromBody] List<BrandDto> brands)
        {
            try
            {
                var createdBrands = await _brandService.CreateBrandsAsync(brands);
                // Return a 201 Created response with the list of created brands
                return CreatedAtAction(nameof(GetBrands), new { }, createdBrands);
            }
            catch (DomainException ex)
            {
                // Catches domain-specific exceptions and returns a 400 Bad Request
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Gets a brand by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the brand to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(Guid id)
        {
            try
            {
                var brand = await _brandService.GetBrandAsync(id);
                return Ok(brand);
            }
            catch (BrandNotFoundException)
            {
                return NotFound();
            }
        }


        /// <summary>
        /// Gets multiple brands by their unique IDs or all brands if no IDs are provided.
        /// </summary>
        /// <param name="ids">A comma-separated string of brand IDs.</param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands([FromQuery] string ids)
        {
            var brands = await _brandService.GetBrandsAsync(ids);
            return Ok(brands);
        }

        /// <summary>
        /// Gets multiple brands by their unique IDs or all brands if no IDs are provided.
        /// </summary>
        /// <param name="ids">A comma-separated string of brand IDs.</param>
        [HttpGet("GetAllBrands")]
        public async Task<ActionResult<IEnumerable<Brand>>> GetAllBrands()
        {
            var brands = await _brandService.GetAllAsync(CancellationToken.None);
            return Ok(brands);
        }

        /// <summary>
        /// Renames an existing brand.
        /// </summary>
        /// <param name="id">The ID of the brand to update.</param>
        /// <param name="newName">The new name for the brand.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(Guid id, [FromBody] string newName)
        {
            try
            {
                await _brandService.UpdateBrandAsync(id, newName);
                return NoContent();
            }
            catch (BrandNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a brand.
        /// </summary>
        /// <param name="id">The ID of the brand to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(Guid id)
        {
            try
            {
                await _brandService.DeleteBrandAsync(id);
                return NoContent();
            }
            catch (BrandNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
