using Microsoft.AspNetCore.Mvc;
using SmartKart.CatalogApi.Application.Dto;
using SmartKart.CatalogApi.Application.Exceptions;
using SmartKart.CatalogApi.Application.Interfaces;
using SmartKart.CatalogApi.Domain.Entities;

namespace SmartKart.CatalogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Gets a list of all products with pagination.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of items per page.</param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var products = await _productService.GetAllAsync(pageNumber, pageSize);
            return Ok(products);
        }

        /// <summary>
        /// Gets a product by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product object to create.</param>
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto product)
        {
            await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="product">The updated product object.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductDto product)
        {
            if (id != product.Id)
            {
                return BadRequest("Product ID mismatch.");
            }
            try
            {
                await _productService.UpdateProductAsync(product);
                return NoContent();
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product object to create.</param>
        [HttpPost("CreateProducts")]
        public async Task<ActionResult<ProductDto>> CreateProducts(List<ProductDto> products)
        {
           
            await _productService.AddProductsAsync(products);
            return Created();
        }

    }
}
