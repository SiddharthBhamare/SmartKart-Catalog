using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartKart.CatalogApi.Domain.Entities;

namespace SmartKart.CatalogService.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products"); // Now valid with relational package installed

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            // Owned Value Object: ProductSKU
            builder.OwnsOne(p => p.SKU, sku =>
            {
                sku.Property(s => s.Value)
                   .HasColumnName("SKU")
                   .HasMaxLength(50)
                   .IsRequired();
            });

            // Owned Value Object: Money price
            builder.OwnsOne(p => p.Price, price =>
            {
                price.Property(m => m.Amount)
                     .HasColumnName("Price")
                     .HasColumnType("decimal(18,2)")
                     .IsRequired();
                price.Property(m => m.Currency)
                     .HasColumnName("Currency")
                     .HasMaxLength(3)
                     .IsRequired();
            });

            builder.Property(p => p.Description).HasMaxLength(1000);

            builder.Property(p => p.StockQuantity).IsRequired();

            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.UpdatedAt).IsRequired(false);

            builder.HasOne<Category>()
                   .WithMany()
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Brand>()
                   .WithMany()
                   .HasForeignKey(p => p.BrandId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
