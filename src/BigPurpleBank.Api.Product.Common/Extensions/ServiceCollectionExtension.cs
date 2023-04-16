using BigPurpleBank.Api.Product.Common.Filter;
using BigPurpleBank.Api.Product.Common.Middleware;
using BigPurpleBank.Api.Product.Common.ModelValidation.FieldProcessors;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BigPurpleBank.Api.Product.Common.Extensions;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Add common services to the DI container
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCommonServices(
        this IServiceCollection services)
    {
        services.AddScoped<ValidateModeFilter>();

        services.AddSingleton<IFieldProcessor, DateTimeProcessor>();
        services.Scan(scan =>
            scan.FromAssemblyOf<IFieldProcessor>()
                .AddClasses(classes => classes.AssignableTo<IApiCommonService>()).AsImplementedInterfaces().WithSingletonLifetime());

        return services;
    }

    /// <summary>
    /// Add common middleware to the pipeline
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder AddCommonServices(
        this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.UseMiddleware<VersionValidationMiddleware>();
        return app;
    }
}