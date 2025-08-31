using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartKart.CatalogApi.Application.Interfaces;
using SmartKart.CatalogApi.Domain.Interfaces;
using SmartKart.CatalogApi.Infrastructure.DBContext;
using SmartKart.CatalogApi.Infrastructure.Interfaces;
using SmartKart.CatalogApi.Infrastructure.Repositories;
using SmartKart.CatalogApi.Infrastructure.Services;

namespace SmartKart.CatalogApi.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var cs = configuration.GetConnectionString("CatalogConnection")
                     ?? configuration["ConnectionStrings:CatalogConnection"]
                     ?? throw new InvalidOperationException("CatalogConnection is not configured.");

            services.AddDbContext<CatalogDbContext>(options =>
                options.UseSqlServer(cs, b => b.MigrationsAssembly(typeof(CatalogDbContext).Assembly.FullName)));

            // generic repository
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            // concrete repositories (optional but convenient)
            services.AddScoped<ProductRepository>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<BrandRepository>();

            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // caching + event dispatcher
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();
            //services.AddScoped<IEventDispatcher, EventDispatcher>();

            return services;
        }
    }
}
