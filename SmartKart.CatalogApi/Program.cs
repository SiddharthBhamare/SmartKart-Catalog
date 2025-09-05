using Microsoft.EntityFrameworkCore;
using SmartKart.CatalogApi.Application.Interfaces;
using SmartKart.CatalogApi.Application.Services;
using SmartKart.CatalogApi.Domain.Interfaces;
using SmartKart.CatalogApi.Infrastructure.DBContext;
using SmartKart.CatalogApi.Infrastructure.Repositories;
using SmartKart.CatalogApi.Infrastructure.Services;
using SmartKart.CatalogApi.Infrastructure; // New using statement for the extensions

namespace SmartKart.CatalogApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Use the new extension method to add Swagger, Authentication, and Authorization services
            builder.Services.AddSwaggerAndAuthentication(builder.Configuration);

            // Add other services here
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<IPaginationService, PaginationService>();
            builder.Services.AddScoped<IProductService, ProductService>();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<CatalogDbContext>(options =>
                options.UseSqlServer(connectionString));

            var app = builder.Build();

            // Use the new extension method to configure the middleware
            app.UseSwaggerAndAuthentication();

            app.MapControllers();
            app.Run();
        }
    }
}
