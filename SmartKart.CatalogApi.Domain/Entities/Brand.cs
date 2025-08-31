using SmartKart.CatalogApi.Domain.Exceptions;
using SmartKart.CatalogApi.Domain.Interfaces;

namespace SmartKart.CatalogApi.Domain.Entities
{
    public class Brand : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; set; }
        private Brand() { } // EF Core

        public Brand(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Brand name cannot be empty.");

            Id = Guid.NewGuid();
            Name = name;
        }

        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new DomainException("Brand name cannot be empty.");

            Name = newName;
        }
    }

}
