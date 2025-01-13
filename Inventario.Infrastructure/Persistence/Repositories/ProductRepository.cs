using Inventario.Domain.Entities;
using Inventario.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Infrastructure.Persistence.Repositories
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.ToListAsync();

        public async Task<Product?> GetByIdAsync(long id) => await _context.Products.FindAsync(id);

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
