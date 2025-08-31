namespace SmartKart.CatalogApi.Application.Dto
{
    public sealed class ProductDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public string Sku { get; init; } = default!;
        public decimal Price { get; init; }
        public string Currency { get; init; } = "USD";
        public string? Description { get; init; }
        public Guid CategoryId { get; init; }
        public Guid BrandId { get; init; }
        public int StockQuantity { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
