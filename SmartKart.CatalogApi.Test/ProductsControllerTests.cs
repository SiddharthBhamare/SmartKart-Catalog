using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartKart.CatalogApi.Application.Dto;
using SmartKart.CatalogApi.Application.Interfaces;
using SmartKart.CatalogApi.Controllers;
using SmartKart.CatalogApi.Domain.Entities;

namespace SmartKart.CatalogApi.Test
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _mockService;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockService = new Mock<IProductService>();
            _controller = new ProductsController(_mockService.Object);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnOk()
        {
            _mockService.Setup(s => s.GetAllAsync(1, 10))
                        .ReturnsAsync(new List<ProductDto> { new ProductDto { Id = Guid.NewGuid(), Name = "iPhone" } });

            var result = await _controller.GetProducts();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var products = Assert.IsType<List<ProductDto>>(okResult.Value);
            Assert.Single(products);
        }

        [Fact]
        public async Task GetProduct_ShouldReturnOk_WhenExists()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.GetByIdAsync(id))
                        .ReturnsAsync(new ProductDto { Id = id, Name = "MacBook" });

            var result = await _controller.GetProduct(id);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<ProductDto>(okResult.Value);
        }

        [Fact]
        public async Task GetProduct_ShouldReturnNotFound_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.GetByIdAsync(id)).ReturnsAsync((ProductDto?)null);

            var result = await _controller.GetProduct(id);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnCreated()
        {
            var product = new ProductDto { Id = Guid.NewGuid(), Name = "Tablet" };
            _mockService.Setup(s => s.AddProductAsync(product)).Returns(Task.CompletedTask);

            var result = await _controller.CreateProduct(product);

            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task UpdateProduct_ShouldReturnNoContent_WhenValid()
        {
            var product = new ProductDto { Id = Guid.NewGuid(), Name = "Watch" };
            _mockService.Setup(s => s.UpdateProductAsync(product)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateProduct(product.Id, product);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_ShouldReturnBadRequest_WhenIdMismatch()
        {
            var product = new ProductDto { Id = Guid.NewGuid(), Name ="Watch" };
            var result = await _controller.UpdateProduct(Guid.NewGuid(), product);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Product ID mismatch.", badRequest.Value);
        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnNoContent_WhenValid()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteProductAsync(id)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteProduct(id);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
