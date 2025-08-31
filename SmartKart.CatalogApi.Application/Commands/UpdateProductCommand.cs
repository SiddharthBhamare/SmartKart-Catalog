namespace SmartKart.CatalogApi.Application.Commands
{
    public sealed class UpdateProductCommand
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public decimal Price { get; init; }
        public string Currency { get; init; } = "USD";
        public string? Description { get; init; }
        public Guid CategoryId { get; init; }
        public Guid BrandId { get; init; }
    }
}
