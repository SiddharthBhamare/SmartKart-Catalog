namespace SmartKart.CatalogApi.Application.Dto
{
    public sealed class BrandDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public string Description { get; init; }
    }
}
