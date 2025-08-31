using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartKart.CatalogApi.Domain.Entities;

namespace SmartKart.CatalogApi.Infrastructure.DBContext.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(b => b.Description)
                   .HasMaxLength(500);

            builder.HasMany<Product>()
                   .WithOne()
                   .HasForeignKey(p => p.BrandId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
