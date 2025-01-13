using Inventario.Domain.Entities;
using Inventario.Domain.Interfaces;

namespace Inventario.Infrastructure.Services
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task CreateProductAsync(Product product)
        {
            // Configurar las fechas del producto
            product.FechaCreacion = DateTime.UtcNow;
            product.FechaActualizacion = DateTime.UtcNow;

            await _productRepository.AddAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            var existingProduct = await _productRepository.GetByIdAsync(product.Id) ?? throw new KeyNotFoundException($"El producto con ID {product.Id} no existe.");

            // Actualizar las propiedades del producto existente
            existingProduct.Nombre = product.Nombre;
            existingProduct.Descripcion = product.Descripcion;
            existingProduct.Precio = product.Precio;
            existingProduct.Cantidad = product.Cantidad;
            existingProduct.FechaActualizacion = DateTime.UtcNow;

            await _productRepository.UpdateAsync(existingProduct);
        }

        public async Task DeleteProductAsync(long id)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);

            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"El producto con ID {id} no existe.");
            }

            await _productRepository.DeleteAsync(id);
        }

        public async Task<Product> GetProductByIdAsync(long id)
        {
            var product = await _productRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"El producto con ID {id} no existe.");
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }
    }
}
