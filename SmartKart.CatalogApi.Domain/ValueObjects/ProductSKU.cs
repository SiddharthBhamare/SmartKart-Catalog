using SmartKart.CatalogApi.Domain.Exceptions;

namespace SmartKart.CatalogApi.Domain.ValueObjects
{
    public record ProductSKU
    {
        public string Value { get; }

        public ProductSKU(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("SKU cannot be empty.");
            if (value.Length > 20)
                throw new DomainException("SKU cannot exceed 20 characters.");

            Value = value.ToUpperInvariant();
        }

        public override string ToString() => Value;
    }

}
