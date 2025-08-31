using SmartKart.CatalogApi.Application.Dto;
using SmartKart.CatalogApi.Application.Interfaces;
using SmartKart.CatalogApi.Application.Mappers;
using SmartKart.CatalogApi.Domain.Entities;
using System.Linq.Expressions;

namespace SmartKart.CatalogApi.Application.Services
{
    public sealed class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfwork;

        public CategoryService(IUnitOfWork unitOfWork) => _unitOfwork = unitOfWork;

        public async Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var c = await _unitOfwork.Categories.GetByIdAsync(id);
            return c is null ? null : CategoryMapper.ToDto(c);
        }

        public async Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken ct = default)
        {
            var items = await _unitOfwork.Categories.ListAsync();
            return items.Select(CategoryMapper.ToDto).ToList();
        }

        public async Task CreateCategory(CategoryDto category, CancellationToken cancellationToken)
        {
            var cat = new Category(category.Name, category.Description);
            await _unitOfwork.Categories.AddAsync(cat);
        }

        public async Task CreateCategories(List<CategoryDto> categories, CancellationToken cancellationToken)
        {
            foreach (var category in categories)
            {
                var cat = new Category(category.Name, category.Description);
                 await _unitOfwork.Categories.AddAsync(cat);
            }
            await _unitOfwork.SaveChangesAsync();
        }
    }
}
