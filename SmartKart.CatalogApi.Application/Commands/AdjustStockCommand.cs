namespace SmartKart.CatalogApi.Application.Commands
{
    public sealed class AdjustStockCommand
    {
        public Guid Id { get; init; }
        public int Delta { get; init; } // positive or negative
    }
}
