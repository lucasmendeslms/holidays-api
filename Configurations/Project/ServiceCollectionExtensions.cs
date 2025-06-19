using HolidayApi.Configurations.Database;
using HolidayApi.Configurations.Settings;
using HolidayApi.Repositories;
using HolidayApi.Repositories.Interfaces;
using HolidayApi.Services;
using HolidayApi.Services.Interfaces;
using HolidayApi.Strategies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HolidayApi.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder RegisterAppDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddAppSettingsConfiguration(builder.Configuration)
                            .AddDatabaseConfiguration()
                            .AddServiceItems()
                            .AddEndpointsApiExplorer()
                            .AddSwaggerGen()
                            .AddControllers();

            return builder;
        }

        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var appSettings = provider.GetRequiredService<IOptions<AppSettings>>().Value;
            var connectionString = appSettings.ConnectionStrings.Database
            ?? throw new InvalidOperationException("Connection string not found.");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddAppSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            return services;
        }

        public static IServiceCollection AddServiceItems(this IServiceCollection services)
        {
            services.AddTransient<IHolidayStrategy, StateHolidayStrategy>();
            // services.AddTransient<IHolidayStrategy, MunicipalityHolidayStrategy>();
            services.AddTransient<HolidayStrategyContext>();
            services.AddScoped<IHolidayRepository, HolidayRepository>();
            return services;
        }
    }
}


// https://dotnetfullstackdev.medium.com/service-collection-extension-pattern-in-net-core-with-item-services-6db8cf9dcfd6
// https://balta.io/blog/csharp-extension-methods