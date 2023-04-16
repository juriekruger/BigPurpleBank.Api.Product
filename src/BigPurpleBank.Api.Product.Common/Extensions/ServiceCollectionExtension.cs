using BigPurpleBank.Api.Product.Common.Filter;
using BigPurpleBank.Api.Product.Common.Middleware;
using BigPurpleBank.Api.Product.Common.ModelValidation.FieldProcessors;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BigPurpleBank.Api.Product.Common.Extensions;

public static class ServiceCollectionExtension
{
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

    public static IApplicationBuilder AddCommonServices(
        this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.UseMiddleware<VersionValidationMiddleware>();
        return app;
    }
}