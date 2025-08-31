namespace SmartKart.CatalogApi.Application.Exceptions
{
    public sealed class BrandNotFoundException : Exception
    {
        public BrandNotFoundException(Guid id)
            : base($"Brand '{id}' was not found.") { }
    }
}
