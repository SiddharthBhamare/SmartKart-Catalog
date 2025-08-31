using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartKart.CatalogApi.Application.Dto;
using SmartKart.CatalogApi.Application.Interfaces;
using SmartKart.CatalogApi.Controllers;
using SmartKart.CatalogApi.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartKart.CatalogApi.Test
{
    public class CategoriesControllerTests
    {
        private readonly Mock<ICategoryService> _mockService;
        private readonly CategoriesController _controller;

        public CategoriesControllerTests()
        {
            _mockService = new Mock<ICategoryService>();
            _controller = new CategoriesController(_mockService.Object);
        }

        [Fact]
        public async Task CreateCategory_ShouldReturnCreated_WhenValid()
        {
            var dto = new CategoryDto { Id = Guid.NewGuid(), Name = "Electronics" };
            _mockService.Setup(s => s.CreateCategory(dto, default)).Returns(Task.CompletedTask);

            var result = await _controller.CreateCategory(dto);

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task CreateCategory_ShouldReturnBadRequest_WhenDomainException()
        {
            var dto = new CategoryDto { Name = "" };
            _mockService.Setup(s => s.CreateCategory(dto, default))
                        .ThrowsAsync(new DomainException("Invalid"));

            var result = await _controller.CreateCategory(dto);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid", badRequest.Value);
        }

        [Fact]
        public async Task GetCategories_ShouldReturnOkList()
        {
            _mockService.Setup(s => s.GetAllAsync(CancellationToken.None))
                        .ReturnsAsync(new List<CategoryDto> { new CategoryDto { Name = "Fashion" } });

            var result = await _controller.GetCategories();

            var okResult = Assert.IsType<List<CategoryDto>>(result.Value);
            Assert.Single(okResult);
        }

        [Fact]
        public async Task GetCategory_ShouldReturnOk_WhenExists()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.GetByIdAsync(id, CancellationToken.None))
                        .ReturnsAsync(new CategoryDto { Id = id, Name = "Books" });

            var result = await _controller.GetCategory(id);

            var dto = Assert.IsType<CategoryDto>(result.Value);
            Assert.Equal("Books", dto.Name);
        }

        [Fact]
        public async Task GetCategory_ShouldReturnNotFound_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.GetByIdAsync(id, CancellationToken.None)).ReturnsAsync((CategoryDto?)null);

            var result = await _controller.GetCategory(id);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
