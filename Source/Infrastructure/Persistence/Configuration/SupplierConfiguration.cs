using Microsoft.EntityFrameworkCore;
using Source.Domain.Entitites;

namespace Source.Infrastructure.Persistence.Configuration
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("suppliers");
            builder.HasKey(s => s.code);

            builder.Property(s => s.code)
                .HasColumnName("code")
                .IsRequired();

            builder.Property(s => s.name)
                .HasColumnName("name")
                .IsRequired();
        }
    }
}