using AutoMapper;
using Inventario.Application.DTOs;
using Inventario.Domain.Entities;

namespace Inventario.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeo de Product a ProductDto
            CreateMap<Product, ProductDto>();

            // Mapeo de ProductDto a Product
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.FechaCreacion, opt => opt.Ignore()) // No mapeamos la fecha de creación
                .ForMember(dest => dest.FechaActualizacion, opt => opt.Ignore()); // No mapeamos la fecha de actualización
        }
    }
}
