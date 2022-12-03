using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MP.ApiDotNet6.Application.Mappings;
using MP.ApiDotNet6.Application.Services;
using MP.ApiDotNet6.Application.Services.Interface;
using MP.ApiDotNet6.Domain.Repositories;
using MP.ApiDotNet6.Infra.Data.Context;
using MP.ApiDotNet6.Infra.Data.Repositories;

namespace MP.ApiDotNet6.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
                                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            //Injeção de dependencia dos repositórios
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();

            return services;
        }

        public static IServiceCollection AddService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(DomainToDtoMapping));

            //Injeção de dependencia dos services
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPurchaseService, PurchaseService>();

            return services;
        }
        
    }
}
