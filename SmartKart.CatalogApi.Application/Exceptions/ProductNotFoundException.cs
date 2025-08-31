namespace SmartKart.CatalogApi.Application.Exceptions
{
    public sealed class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(Guid id)
            : base($"Product '{id}' was not found.") { }
    }
}
