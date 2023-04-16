using System.Net;
using System.Text.Json;
using BigPurpleBank.Api.Product.Common.Constants;
using BigPurpleBank.Api.Product.Common.Extensions;
using BigPurpleBank.Api.Product.Common.Model;
using BigPurpleBank.Api.Product.Data.Repositories;
using BigPurpleBank.Api.Product.Model;
using BigPurpleBank.Api.Product.Model.Dto;
using BigPurpleBank.Api.Product.Model.Enum;
using BigPurpleBank.Api.Product.Model.Responses.Product;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace BigPurpleBank.Api.Product.Tests.Integration.Controllers;

public partial class ProductControllerTests : IClassFixture<ProductApiFactory>
{
    private readonly ProductApiFactory _factory;
    private readonly JsonSerializerOptions _jsonOptions;

    public ProductControllerTests(
        ProductApiFactory factory)
    {
        _factory = factory;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }


    [Fact]
    public async Task Get_WithNoProducts_ReturnsOk()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "/v3/banking/Product");
        request.Headers.Add(HeaderNames.Version, "3");
        // Act
        var response = await client.SendAsync(request);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Get_WithProductCategory_ReturnsOk()
    {
        var faker = new Faker<ProductDto>()
            .RuleFor(x => x.ProductCategory, ProductCategory.Leases)
            .Generate();

        var productRepository = _factory.Services.GetRequiredService<IProductRepository>();
        await productRepository.AddItemAsync(faker);

        var url = "/v3/banking/Product?product-category=LEASES";
        var response = await SendRequestAsync(url);
        
        var responseModel = await ValidateBaseResponse(response);
        responseModel.Data.All(x => x.ProductCategory == ProductCategory.Leases).ShouldBeTrue();
    }

    [Fact]
    public async Task Get_WithEffectiveFutureDate_ReturnsOk()
    {
        var faker = new Faker<ProductDto>()
            .RuleFor(x => x.EffectiveFromUnix, f => f.Date.Soon().ToUnixTime())
            .Generate();

        var productRepository = _factory.Services.GetRequiredService<IProductRepository>();
        await productRepository.AddItemAsync(faker);


        var url ="/v3/banking/Product?effective=future";
        var response = await SendRequestAsync(url);
        
        var responseModel = await ValidateBaseResponse(response);
        responseModel.Data.All(x => x.EffectiveFrom >= DateTime.Now).ShouldBeTrue();
    }


    [Fact]
    public async Task Get_WithEffective_CurrentDate_ReturnsOk()
    {
        var faker = new Faker<ProductDto>()
            .RuleFor(x => x.EffectiveFromUnix, f => f.Date.Past().ToUnixTime())
            .Generate();

        var productRepository = _factory.Services.GetRequiredService<IProductRepository>();
        await productRepository.AddItemAsync(faker);

        var url = "/v3/banking/Product?effective=Current";
        var response = await SendRequestAsync(url);
        var responseModel = await ValidateBaseResponse(response);
        responseModel.Data.All(x => x.EffectiveFrom <= DateTime.Now).ShouldBeTrue();
    }
    
    
    [Fact]
    public async Task Get_WithUpdatedSince_ReturnsOk()
    {
        var faker = new Faker<ProductDto>()
            .RuleFor(x => x.LastUpdatedUnix, f => DateTime.Now.AddDays(-1).ToUnixTime())
            .Generate();

        var productRepository = _factory.Services.GetRequiredService<IProductRepository>();
        await productRepository.AddItemAsync(faker);

        var url = $"/v3/banking/Product?updated-since={DateTime.Now.AddDays(-1):yyyy-MM-dd}";
        var response = await SendRequestAsync(url);
        
        var responseModel = await ValidateBaseResponse(response);
        responseModel.Data.All(x => x.LastUpdated <= DateTime.Now.AddDays(-1)).ShouldBeTrue();
    }

    private async Task<BaseResponseModel<IEnumerable<ProductViewModel>>> ValidateBaseResponse(
        HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseModel = JsonSerializer.Deserialize<BaseResponseModel<IEnumerable<ProductViewModel>>>(responseContent, _jsonOptions);

        // Assert
        responseModel.ShouldNotBeNull();
        responseModel.Data.ShouldNotBeEmpty();
        return responseModel;
    }

    private async Task<HttpResponseMessage> SendRequestAsync(
        string url)
    {
        var client = _factory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add(HeaderNames.Version, "3");

        var response = await client.SendAsync(request);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        return response;
    }
    
}