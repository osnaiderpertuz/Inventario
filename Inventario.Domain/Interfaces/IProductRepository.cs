using Inventario.Domain.Entities;

namespace Inventario.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(long id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(long id);
    }
}
