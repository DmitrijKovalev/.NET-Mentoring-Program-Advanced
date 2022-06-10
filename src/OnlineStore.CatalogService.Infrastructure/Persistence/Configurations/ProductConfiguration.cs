using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.CatalogService.Domain.Entities;

namespace OnlineStore.CatalogService.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Product entity configuration.
    /// </summary>
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Price)
                .IsRequired();

            builder.Property(p => p.Amount)
                .IsRequired();

            builder
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .IsRequired();
        }
    }
}
