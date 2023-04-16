using System.Net;
using BigPurpleBank.Api.Product.Common.Model.Config;
using BigPurpleBank.Api.Product.Data.Factories;
using BigPurpleBank.Api.Product.Data.Repositories;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BigPurpleBank.Api.Product.Data.Extensions;

public static class ServiceCollectionExtension
{

    public static IServiceCollection AddDataServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        
        var cosmosDbConfig = configuration.GetSection("CosmosDbSettings").Get<CosmosDbSettings>();
        var client = new CosmosClient(cosmosDbConfig.EndpointUrl, cosmosDbConfig.PrimaryKey, new CosmosClientOptions()
        {
            SerializerOptions = new CosmosSerializationOptions()
            {
                PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
            }
        });
        var cosmosDbClientFactory = new CosmosDbContainerFactory(client, cosmosDbConfig.DatabaseName, cosmosDbConfig.Containers);
        services.AddSingleton<ICosmosDbContainerFactory>(cosmosDbClientFactory);
        

        services.Scan(scan =>
            scan.FromAssembliesOf(typeof(IDbContextRepository<>))
                .AddClasses(classes =>
                    classes.AssignableTo(typeof(IDbContextRepository<>)).Where(_ => !_.IsGenericType))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return services;
    }
}