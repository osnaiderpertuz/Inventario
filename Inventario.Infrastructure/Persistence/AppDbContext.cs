using Inventario.Domain.Entities;
using Inventario.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.Entity<Product>()
            .Property(e => e.FechaActualizacion)
            .HasConversion(
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );
            modelBuilder.Entity<Product>()
            .Property(e => e.FechaCreacion)
            .HasConversion(
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );
        }
    }
}
