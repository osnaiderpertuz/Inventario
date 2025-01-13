using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Inventario.Application.DTOs;
using Inventario.Domain.Entities;
using Inventario.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Inventario.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IValidator<ProductDto> _productDtoValidator;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductService productService, IMapper mapper, IValidator<ProductDto> productDtoValidator, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _productDtoValidator = productDtoValidator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Obteniendo todos los usuarios.");
            try
            {
                var products = await _productService.GetAllProductsAsync();

                // Mapear de List<Product> a List<ProductDto>
                return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos.");
                return StatusCode(500, "Hubo un error interno en el servidor.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            _logger.LogInformation("ID de Usuario a obtener: {Id}", id);
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return product != null ? Ok(_mapper.Map<ProductDto>(product)) : NotFound();
            }
            catch(KeyNotFoundException kEx)
            {
                _logger.LogWarning(kEx, kEx.Message);
                return NotFound(kEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener producto.");
                return StatusCode(500, "Hubo un error interno en el servidor.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto productDto)
        {
            _logger.LogInformation("Usuario a crear:\n{ProductDto}", productDto);
            try
            {
                // Validar el DTO
                ValidationResult validationResult = _productDtoValidator.Validate(productDto);

                if (!validationResult.IsValid)
                {
                    string errores = string.Join(',', validationResult.Errors.Select(e => e.ErrorMessage));
                    _logger.LogWarning("Errores de validación en creación: {Errores}", errores);

                    // Devolver errores de validación
                    return BadRequest(validationResult.Errors.Select(error => new
                    {
                        Field = error.PropertyName,
                        Error = error.ErrorMessage
                    }));
                }

                // Mapear de ProductDto a Product
                var product = _mapper.Map<Product>(productDto);

                await _productService.CreateProductAsync(product);
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear producto.");
                return StatusCode(500, "Hubo un error interno en el servidor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] ProductDto productDto)
        {
            _logger.LogInformation("ID de Usuario a actualizar: {Id}. DTO:\n{ProductDto}", id, productDto);
            try
            {
                // Validar el DTO
                ValidationResult validationResult = _productDtoValidator.Validate(productDto);

                if (!validationResult.IsValid)
                {
                    string errores = string.Join(',', validationResult.Errors.Select(e => e.ErrorMessage));
                    _logger.LogWarning("Errores de validación en actualización: {Errores}", errores);

                    // Devolver errores de validación
                    return BadRequest(validationResult.Errors.Select(error => new
                    {
                        Field = error.PropertyName,
                        Error = error.ErrorMessage
                    }));
                }

                // Mapear de ProductDto a Product
                var product = _mapper.Map<Product>(productDto);

                if (id != product.Id) return BadRequest("IDs no coinciden.");
                await _productService.UpdateProductAsync(product);
                return NoContent();
            }
            catch (KeyNotFoundException kEx)
            {
                _logger.LogWarning(kEx, kEx.Message);
                return NotFound(kEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar producto.");
                return StatusCode(500, "Hubo un error interno en el servidor.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            _logger.LogInformation("ID de Usuario a eliminar: {Id}", id);
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException kEx)
            {
                _logger.LogWarning(kEx, kEx.Message);
                return NotFound(kEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar producto.");
                return StatusCode(500, "Hubo un error interno en el servidor.");
            }
        }
    }
}
