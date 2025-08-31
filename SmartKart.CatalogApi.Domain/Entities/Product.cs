using SmartKart.CatalogApi.Domain.Exceptions;
using SmartKart.CatalogApi.Domain.Interfaces;
using SmartKart.CatalogApi.Domain.ValueObjects;

namespace SmartKart.CatalogApi.Domain.Entities
{
    public class Product : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public ProductSKU SKU { get; private set; }
        public Money Price { get; private set; }
        public string Description { get; private set; }
        public Guid CategoryId { get; private set; }
        public Guid BrandId { get; private set; }
        public int StockQuantity { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private Product() { } // EF Core

        public Product(string name, ProductSKU sku, Money price, string description, Guid categoryId, Guid brandId, int stockQuantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Product name cannot be empty.");

            Id = Guid.NewGuid();
            Name = name;
            SKU = sku;
            Price = price;
            Description = description;
            CategoryId = categoryId;
            BrandId = brandId;
            StockQuantity = stockQuantity;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(string name, Money price, string description, Guid categoryId, Guid brandId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Product name cannot be empty.");

            Name = name;
            Price = price;
            Description = description;
            CategoryId = categoryId;
            BrandId = brandId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AdjustStock(int quantity)
        {
            if (StockQuantity + quantity < 0)
                throw new DomainException("Insufficient stock.");
            StockQuantity += quantity;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Update(string name, object sku, Money price, string description, Guid categoryId, Guid brandId, int stockQuantity)
        {
            throw new NotImplementedException();
        }
    }

}
