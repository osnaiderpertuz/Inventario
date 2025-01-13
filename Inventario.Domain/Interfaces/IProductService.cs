using Inventario.Domain.Entities;

namespace Inventario.Domain.Interfaces
{
    public interface IProductService
    {
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(long id);
        Task<Product> GetProductByIdAsync(long id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
