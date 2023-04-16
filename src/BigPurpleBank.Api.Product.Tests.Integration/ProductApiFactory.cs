using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BigPurpleBank.Api.Product.Tests.Integration;

public class ProductApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(
        IWebHostBuilder builder)
    {
        builder.ConfigureServices(services => { });

        builder.UseEnvironment("Development");
    }
}