using Alza.Database.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alza.Database.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id");
        builder.Property(x => x.Name).HasMaxLength(64).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(512);
        builder.Property(x => x.Price).HasDefaultValue(100).IsRequired();
    }
}
