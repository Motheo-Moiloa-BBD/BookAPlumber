using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using BookAPlumber.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

            return services;
        }
    }
}
