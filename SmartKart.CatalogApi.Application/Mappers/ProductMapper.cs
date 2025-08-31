using SmartKart.CatalogApi.Application.Dto;
using SmartKart.CatalogApi.Domain.Entities;
using SmartKart.CatalogApi.Domain.ValueObjects;

namespace SmartKart.CatalogApi.Application.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(Product p) => new()
        {
            Id = p.Id,
            Name = p.Name,
            Sku = p.SKU.Value,
            Price = p.Price.Amount,
            Currency = p.Price.Currency,
            Description = p.Description,
            CategoryId = p.CategoryId,
            BrandId = p.BrandId,
            StockQuantity = p.StockQuantity,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        };


        public static Product FromCreate(ProductDto product)
        {
            return FromCreate(product.Name, product.Sku, product.Price, product.Currency, product.Description, product.CategoryId, product.BrandId, product.StockQuantity);
        }
        public static Product FromCreate(string name, string sku, decimal price, string currency,
                                         string? description, Guid categoryId, Guid brandId, int initialStock)
        {
            var money = new Money(price, currency);
            var productSku = new ProductSKU(sku);
            return new Product(name, productSku, money, description ?? string.Empty, categoryId, brandId, initialStock);
        }

        public static void ApplyUpdate(Product p, string name, decimal price, string currency,
                                       string? description, Guid categoryId, Guid brandId)
        {
            var money = new Money(price, currency);
            p.UpdateDetails(name, money, description ?? string.Empty, categoryId, brandId);
        }
    }
}
