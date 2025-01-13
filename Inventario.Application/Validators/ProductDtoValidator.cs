using FluentValidation;
using Inventario.Application.DTOs;

namespace Inventario.Application.Validators
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("El nombre del producto es obligatorio.")
                .MaximumLength(100)
                .WithMessage("El nombre del producto no puede superar los 100 caracteres.");

            RuleFor(x => x.Precio)
                .GreaterThan(0)
                .WithMessage("El precio del producto debe ser mayor a cero.");

            RuleFor(x => x.Cantidad)
                .GreaterThan(0)
                .WithMessage("La cantidad del producto debe ser mayor a cero.");
        }
    }
}
