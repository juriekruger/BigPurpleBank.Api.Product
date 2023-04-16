using System.Net;
using BigPurpleBank.Api.Product.Common.Exceptions;
using BigPurpleBank.Api.Product.Common.Filter;
using BigPurpleBank.Api.Product.Common.Model;
using BigPurpleBank.Api.Product.Common.ModelValidation.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace BigPurpleBank.Api.Product.Common.Tests.Filter;

public class ValidateModeFilterTests
{
    private readonly Mock<IModelValidationErrorFactory> _errorFactoryMock;
    private readonly ValidateModeFilter _sut;

    public ValidateModeFilterTests()
    {
        _errorFactoryMock = new Mock<IModelValidationErrorFactory>();
        _errorFactoryMock.Setup(x => x.ProcessModelState(It.IsAny<ModelStateDictionary>())).Returns(new List<Error> { new() });
        _sut = new ValidateModeFilter(_errorFactoryMock.Object);
    }

    [Fact]
    public void OnActionExecuting_WhenModelStateIsValid_DoesNotThrow()
    {
        // Arrange
        var context = new ActionExecutingContext(
            new ActionContext(
                new DefaultHttpContext(),
                new RouteData(),
                new ActionDescriptor()),
            new List<IFilterMetadata>(),
            new Dictionary<string, object>(),
            null);

        // Act
        _sut.OnActionExecuting(context);

        // Assert
        Assert.True(context.ModelState.IsValid);
    }

    [Fact]
    public void OnActionExecuting_WhenModelStateIsInvalid_CallsErrorFactory()
    {
        // Arrange
        var context = new ActionExecutingContext(
            new ActionContext(
                new DefaultHttpContext(),
                new RouteData(),
                new ActionDescriptor()),
            new List<IFilterMetadata>(),
            new Dictionary<string, object>(),
            null);

        context.ModelState.AddModelError("key", "error");

        // Act
        Record.Exception(() => _sut.OnActionExecuting(context));

        // Assert
        _errorFactoryMock.Verify(x => x.ProcessModelState(It.IsAny<ModelStateDictionary>()), Times.Once);
    }

    [Fact]
    public void OnActionExecuting_WhenModelStateIsInvalid_ThrowsBadRequestExceptionWithErrors()
    {
        // Arrange
        var context = new ActionExecutingContext(
            new ActionContext(
                new DefaultHttpContext(),
                new RouteData(),
                new ActionDescriptor()),
            new List<IFilterMetadata>(),
            new Dictionary<string, object>(),
            null);

        context.ModelState.AddModelError("key", "error");

        // Act
        var exception = Record.Exception(() => _sut.OnActionExecuting(context));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<BadRequestException>(exception);
        Assert.NotNull(((BadRequestException)exception).Errors);
        Assert.NotEmpty(((BadRequestException)exception).Errors);
        Assert.Equal(HttpStatusCode.BadRequest, ((BadRequestException)exception).HttpStatusCode);
    }
}