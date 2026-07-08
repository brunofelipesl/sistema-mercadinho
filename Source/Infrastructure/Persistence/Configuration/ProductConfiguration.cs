using Microsoft.EntityFrameworkCore;
using Source.Domain.Entitites;

namespace Source.Infrastructure.Persistence.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");
            builder.HasKey(p => p.code);

            builder.Property(p => p.code)
                .HasColumnName("code")
                .IsRequired();

            builder.Property(p => p.description)
                .HasColumnName("description")
                .IsRequired();

            builder.Property(p => p.sellingPrice)
                .HasColumnName("selling_price")
                .IsRequired();

            builder.Property(p => p.replacementCost)
                .HasColumnName("replacement_cost")
                .IsRequired();

            builder.Property(p => p.expirationDate)
                .HasColumnName("expiration_date")
                .IsRequired();

            builder.Property(p => p.stockQuantity)
                .HasColumnName("stock_quantity")
                .IsRequired();

            builder.HasMany(p => p.categories)
                   .WithMany()
                   .UsingEntity(j => j.ToTable("product_categories"));

            builder.HasMany(p => p.suppliers)
                   .WithMany()
                   .UsingEntity(j => j.ToTable("product_suppliers"));
        }
    }
}