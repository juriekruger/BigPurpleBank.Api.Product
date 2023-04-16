using BigPurpleBank.Api.Product.Data.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BigPurpleBank.Api.Product.Services.Extensions;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Add all API services to the DI container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDataServices(configuration);
        services.Scan(scan =>
            scan.FromAssemblyOf<IApiService>().AddClasses(classes => classes.AssignableTo<IApiService>()).AsImplementedInterfaces().WithTransientLifetime());

        return services;
    }
}