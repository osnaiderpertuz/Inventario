using Inventario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventario.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
               .ValueGeneratedOnAdd()
               .UseIdentityByDefaultColumn();

            builder.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Descripcion).HasMaxLength(500);
            builder.Property(p => p.Precio).HasColumnType("decimal(18,2)");
            builder.Property(p => p.FechaCreacion).IsRequired();
            builder.Property(p => p.FechaActualizacion).IsRequired();
        }
    }
}
