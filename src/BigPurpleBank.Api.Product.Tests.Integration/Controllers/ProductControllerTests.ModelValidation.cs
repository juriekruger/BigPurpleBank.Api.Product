using System.Net;
using System.Text.Json;
using BigPurpleBank.Api.Product.Common.Constants;
using BigPurpleBank.Api.Product.Common.Model;
using Shouldly;

namespace BigPurpleBank.Api.Product.Tests.Integration.Controllers;

public partial class ProductControllerTests
{
    
    [Fact]
    public async Task Get_WithInvalidEffective_ReturnsBadRequest()
    {
        var url = "/v3/banking/Product?effective=invalid";
        var responseModel = await SendAndValidateRequestAsync(url);
        
        responseModel.ShouldNotBeNull();
        responseModel.Errors.ShouldNotBeEmpty();
        responseModel.Errors[0].ShouldBeEquivalentTo(new Error
        {
            Code = "urn:au-cds:error:cds-all:Field/Invalid",
            Title = "Invalid Field",
            Detail = "The value 'invalid' is not valid for Effective."
        });
    }

    private async Task<ErrorResponse?> SendAndValidateRequestAsync(
        string url)
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add(HeaderNames.Version, "3");
        // Act
        var response = await client.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseModel = JsonSerializer.Deserialize<ErrorResponse>(responseContent);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        return responseModel;
    }

    [Fact]
    public async Task Get_WithInvalidUpdatedSince_ReturnsBadRequest()
    {
        var url ="/v3/banking/Product?updated-since=invalid";
        var responseModel = await SendAndValidateRequestAsync(url);
        responseModel.ShouldNotBeNull();
        responseModel.Errors.ShouldNotBeEmpty();
        responseModel.Errors[0].ShouldBeEquivalentTo(new Error
        {
            Code = "urn:au-cds:error:cds-all:Field/Invalid",
            Title = "Invalid Field",
            Detail = "The value 'invalid' is not valid for UpdatedSince."
        });
    }

    [Fact]
    public async Task Get_WithInvalidProductCategory_ReturnsBadRequest()
    {
        var url ="/v3/banking/Product?product-category=invalid";
        var responseModel = await SendAndValidateRequestAsync(url);
        responseModel.ShouldNotBeNull();
        responseModel.Errors.ShouldNotBeEmpty();
        responseModel.Errors[0].ShouldBeEquivalentTo(new Error
        {
            Code = "urn:au-cds:error:cds-all:Field/Invalid",
            Title = "Invalid Field",
            Detail = "The value 'invalid' is not valid for ProductCategory."
        });
    }

    [Fact]
    public async Task Get_WithInvalidPage_ReturnsBadRequest()
    {
        var url ="/v3/banking/Product?page=-1";
        var responseModel = await SendAndValidateRequestAsync(url);
        responseModel.ShouldNotBeNull();
        responseModel.Errors.ShouldNotBeEmpty();
        responseModel.Errors[0].ShouldBeEquivalentTo(new Error
        {
            Code = "urn:au-cds:error:cds-all:Field/Invalid",
            Title = "Invalid Field",
            Detail = "The field Page must be between 0 and 2147483647."
        });
    }

    [Fact]
    public async Task Get_WithInvalidPageSize_ReturnsBadRequest()
    {
        var url ="/v3/banking/Product?page-size=0";
        var responseModel = await SendAndValidateRequestAsync(url);
        responseModel.ShouldNotBeNull();
        responseModel.Errors.ShouldNotBeEmpty();
        responseModel.Errors[0].ShouldBeEquivalentTo(new Error
        {
            Code = "urn:au-cds:error:cds-all:Field/Invalid",
            Title = "Invalid Field",
            Detail = "The field PageSize must be between 1 and 1000."
        });
    }
}