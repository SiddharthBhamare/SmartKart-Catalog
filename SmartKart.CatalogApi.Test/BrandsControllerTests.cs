using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartKart.CatalogApi.Application.Dto;
using SmartKart.CatalogApi.Application.Exceptions;
using SmartKart.CatalogApi.Application.Interfaces;
using SmartKart.CatalogApi.Controllers;
using SmartKart.CatalogApi.Domain.Entities;
using SmartKart.CatalogApi.Domain.Exceptions;

namespace SmartKart.CatalogApi.Test
{
    public class BrandsControllerTests
    {
        private readonly Mock<IBrandService> _mockService;
        private readonly BrandsController _controller;

        public BrandsControllerTests()
        {
            _mockService = new Mock<IBrandService>();
            _controller = new BrandsController(_mockService.Object);
        }

        [Fact]
        public async Task CreateBrand_ShouldReturnCreated_WhenValid()
        {
            var dto = new BrandDto { Name = "Nike", Description = "Sports" };
            var brand = new Brand(dto.Name) { Description = dto.Description };

            _mockService.Setup(s => s.CreateBrandAsync(dto)).ReturnsAsync(dto);

            var result = await _controller.CreateBrand(dto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetBrand", createdResult.ActionName);
        }

        [Fact]
        public async Task CreateBrand_ShouldReturnBadRequest_WhenDomainException()
        {
            var dto = new BrandDto { Name = "" };
            _mockService.Setup(s => s.CreateBrandAsync(dto))
                        .ThrowsAsync(new DomainException("Invalid"));

            var result = await _controller.CreateBrand(dto);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid", badRequest.Value);
        }

        [Fact]
        public async Task GetBrand_ShouldReturnOk_WhenExists()
        {
            var id = Guid.NewGuid();
            var dto = new BrandDto { Id = id, Name = "Puma" };

            _mockService.Setup(s => s.GetBrandAsync(id)).ReturnsAsync(dto);

            var result = await _controller.GetBrand(id);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedDto = Assert.IsType<BrandDto>(okResult.Value);
            Assert.Equal("Puma", returnedDto.Name);
        }

        [Fact]
        public async Task GetBrand_ShouldReturnNotFound_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.GetBrandAsync(id))
                        .ThrowsAsync(new BrandNotFoundException(id));

            var result = await _controller.GetBrand(id);

            Assert.IsType<NotFoundResult>(result.Result);
        }


        [Fact]
        public async Task UpdateBrand_ShouldReturnNoContent_WhenValid()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.UpdateBrandAsync(id, "Adidas")).Returns(Task.CompletedTask);

            var result = await _controller.UpdateBrand(id, "Adidas");

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateBrand_ShouldReturnNotFound_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.UpdateBrandAsync(id, "Unknown"))
                        .ThrowsAsync(new BrandNotFoundException(id));

            var result = await _controller.UpdateBrand(id, "Unknown");

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteBrand_ShouldReturnNoContent_WhenValid()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteBrandAsync(id)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteBrand(id);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
