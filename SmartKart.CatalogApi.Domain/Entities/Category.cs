using SmartKart.CatalogApi.Domain.Exceptions;
using SmartKart.CatalogApi.Domain.Interfaces;

namespace SmartKart.CatalogApi.Domain.Entities
{
    public class Category : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        private Category() { } // EF Core

        public Category(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Category name cannot be empty.");

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        public void Update(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Category name cannot be empty.");

            Name = name;
            Description = description;
        }
    }

}
