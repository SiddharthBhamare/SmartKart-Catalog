using SmartKart.CatalogApi.Application.Dto;
using SmartKart.CatalogApi.Domain.Entities;

namespace SmartKart.CatalogApi.Application.Mappers
{
    public static class BrandMapper
    {
        public static BrandDto ToDto(Brand b) => new()
        {
            Id = b.Id,
            Name = b.Name
        };
    }
}
