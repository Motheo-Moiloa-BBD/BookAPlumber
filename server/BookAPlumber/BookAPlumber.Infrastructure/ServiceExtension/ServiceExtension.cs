using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using BookAPlumber.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BookAPlumber.Core.Interfaces;
using BookAPlumber.Infrastructure.Repositories;

namespace BookAPlumber.Infrastructure.ServiceExtension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookAPlumberDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("BookAPlumberConnectionString"));
 
            });

            services.AddDbContext<BookAPlumberAuthDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("BookAPlumberConnectionString"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenRepository, TokenRepository>();

            return services;
        }
    }
}
