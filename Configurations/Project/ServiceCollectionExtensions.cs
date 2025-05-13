using HolidayApi.Configurations.Database;
using HolidayApi.Repositories;
using HolidayApi.Repositories.Interfaces;
using HolidayApi.Services;
using HolidayApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HolidayApi.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder RegisterAppDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddDatabaseConfiguration(builder.Configuration)
                            .AddServiceItems()
                            .AddEndpointsApiExplorer()
                            .AddSwaggerGen()
                            .AddControllers();

            return builder;
        }

        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddServiceItems(this IServiceCollection services)
        {
            services.AddScoped<IHolidayService, HolidayService>();
            services.AddScoped<IHolidayRepository, HolidayRepository>();
            return services;
        }
    }
}


// https://dotnetfullstackdev.medium.com/service-collection-extension-pattern-in-net-core-with-item-services-6db8cf9dcfd6
// https://balta.io/blog/csharp-extension-methods