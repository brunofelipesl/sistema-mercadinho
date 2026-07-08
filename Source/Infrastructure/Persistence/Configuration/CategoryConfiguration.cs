using Microsoft.EntityFrameworkCore;
using Source.Domain.Entitites;

namespace Source.Infrastructure.Persistence.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("categories");
            builder.HasKey(c => c.code);

            builder.Property(c => c.code)
                .HasColumnName("code")
                .IsRequired();

            builder.Property(c => c.description)
                .HasColumnName("description")
                .IsRequired();
        }
    }
}