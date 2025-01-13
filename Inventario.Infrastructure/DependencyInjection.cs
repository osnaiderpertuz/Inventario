using FluentValidation;
using Inventario.Application.DTOs;
using Inventario.Application.MappingProfiles;
using Inventario.Application.Validators;
using Inventario.Domain.Interfaces;
using Inventario.Infrastructure.Persistence;
using Inventario.Infrastructure.Persistence.Repositories;
using Inventario.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Inventario.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IValidator<ProductDto>, ProductDtoValidator>();

            // Configurar AutoMapper
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            return services;
        }
    }
}
