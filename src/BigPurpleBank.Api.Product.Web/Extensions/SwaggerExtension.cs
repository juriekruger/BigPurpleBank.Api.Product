using BigPurpleBank.Api.Product.Web.Swagger;

namespace BigPurpleBank.Api.Product.Web.Extensions;

/// <summary>
///     Adds swagger configuration
/// </summary>
public static class SwaggerExtension
{
    internal static IServiceCollection AddSwaggerConfiguration(
        this IServiceCollection services)
    {
        services.AddSwaggerGen(
            options =>
            {
                // add a custom operation filter which sets default values
                options.OperationFilter<SwaggerDefaultValues>();

                var fileName = typeof(Program).Assembly.GetName().Name + ".xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, fileName);

                // integrate xml comments
                options.IncludeXmlComments(filePath);
            });
        return services;
    }

    internal static WebApplication UseSwaggerConfiguration(
        this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
}