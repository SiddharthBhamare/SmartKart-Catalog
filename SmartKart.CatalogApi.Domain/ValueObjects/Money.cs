using SmartKart.CatalogApi.Domain.Exceptions;

namespace SmartKart.CatalogApi.Domain.ValueObjects
{
    public record Money
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency = "USD")
        {
            if (amount < 0) throw new DomainException("Money cannot be negative.");
            if (string.IsNullOrWhiteSpace(currency)) throw new DomainException("Currency cannot be empty.");

            Amount = amount;
            Currency = currency;
        }

        public static Money Zero(string currency = "USD") => new(0, currency);

        public Money Add(Money other)
        {
            if (Currency != other.Currency)
                throw new DomainException("Currency mismatch.");
            return new Money(Amount + other.Amount, Currency);
        }

        public Money Subtract(Money other)
        {
            if (Currency != other.Currency)
                throw new DomainException("Currency mismatch.");
            return new Money(Amount - other.Amount, Currency);
        }
    }

}
