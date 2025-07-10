using HolidayApi.Configurations.Database;
using HolidayApi.Configurations.Settings;
using HolidayApi.Facades;
using HolidayApi.Facades.Interfaces;
using HolidayApi.Repositories;
using HolidayApi.Repositories.Interfaces;
using HolidayApi.Services;
using HolidayApi.Services.Interfaces;
using HolidayApi.Strategies;
using Microsoft.EntityFrameworkCore;

namespace HolidayApi.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder RegisterAppDependencies(this WebApplicationBuilder builder)
        {
            var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>()
            ?? throw new InvalidOperationException("AppSettings section is missing or invalid.");

            builder.Services.AddSingleton(appSettings)
                            .AddDatabaseConfiguration(appSettings)
                            .AddHttpClient()
                            .AddServiceItems()
                            .AddEndpointsApiExplorer()
                            .AddSwaggerGen()
                            .AddControllers();

            return builder;
        }

        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, AppSettings appSettings)
        {
            var connectionString = appSettings.ConnectionStrings.Database
            ?? throw new InvalidOperationException("Connection string not found.");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddServiceItems(this IServiceCollection services)
        {
            services.AddScoped<IHolidayStrategy, StateHolidayStrategy>();
            services.AddScoped<IHolidayStrategy, MunicipalityHolidayStrategy>();
            services.AddScoped<IMunicipalityRepository, MunicipalityRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IMunicipalityRepository, MunicipalityRepository>();
            services.AddScoped<IIbgeFacade, IbgeFacade>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<IMunicipalityService, MunicipalityService>();
            services.AddTransient<HolidayStrategyContext>();
            services.AddScoped<IHolidayRepository, HolidayRepository>();
            return services;
        }
    }
}


// https://dotnetfullstackdev.medium.com/service-collection-extension-pattern-in-net-core-with-item-services-6db8cf9dcfd6
// https://balta.io/blog/csharp-extension-methods