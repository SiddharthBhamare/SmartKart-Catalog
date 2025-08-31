namespace SmartKart.CatalogApi.Application.Commands
{
    public sealed class CreateProductCommand
    {
        public string Name { get; init; } = default!;
        public string Sku { get; init; } = default!;
        public decimal Price { get; init; }
        public string Currency { get; init; } = "USD";
        public string? Description { get; init; }
        public Guid CategoryId { get; init; }
        public Guid BrandId { get; init; }
        public int InitialStock { get; init; } = 0;
    }
}
