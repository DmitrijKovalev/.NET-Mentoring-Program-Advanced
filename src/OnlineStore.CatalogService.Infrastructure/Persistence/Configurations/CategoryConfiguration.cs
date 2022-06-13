using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.CatalogService.Domain.Entities;

namespace OnlineStore.CatalogService.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Category entity configuration.
    /// </summary>
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
               .HasMaxLength(50)
               .IsRequired();

            builder
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.ChildCategories)
                .HasForeignKey(c => c.ParentCategoryId);
        }
    }
}
