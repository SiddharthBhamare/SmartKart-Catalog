namespace SmartKart.CatalogApi.Application.Exceptions
{
    public sealed class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException(Guid id)
            : base($"Category '{id}' was not found.") { }
    }
}
