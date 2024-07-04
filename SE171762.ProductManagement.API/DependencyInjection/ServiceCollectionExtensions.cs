using Microsoft.EntityFrameworkCore;
using NET171462.ProductManagement.Repo.Models;
using NET171462.ProductManagement.Repo.Repository.Interface;
using NET171462.ProductManagement.Repo.Repository;
using SE171762.ProductManagement.API.Mapper;
using SE171462.ProductManagement.Repo.JwtService.Interface;
using SE171462.ProductManagement.Repo.JwtService;

namespace SE171762.ProductManagement.API.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigRepository(this IServiceCollection services)
            => services.AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddTransient(typeof(CategoryRepository))
                .AddTransient(typeof(ProductRepository))
                .AddTransient<IJwtTokenService, JwtTokenService>();
        public static IServiceCollection AddConfigDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyStoreContext>(options
                => options.UseSqlServer(configuration.GetConnectionString("ConnectionStrings")));
            return services;
        }

        public static IServiceCollection AddConfigurationMapper(this IServiceCollection services)
            => services.AddAutoMapper(typeof(MappingProfile));
    }
}
