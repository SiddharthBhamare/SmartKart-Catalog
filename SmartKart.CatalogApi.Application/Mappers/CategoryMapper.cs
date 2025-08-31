using SmartKart.CatalogApi.Application.Dto;
using SmartKart.CatalogApi.Domain.Entities;

namespace SmartKart.CatalogApi.Application.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ToDto(Category c) => new()
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description
        };
    }
}
