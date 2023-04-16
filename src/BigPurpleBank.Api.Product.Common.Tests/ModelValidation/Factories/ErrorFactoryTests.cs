using BigPurpleBank.Api.Product.Common.ModelValidation.Factories;
using BigPurpleBank.Api.Product.Common.ModelValidation.FieldProcessors;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Shouldly;

namespace BigPurpleBank.Api.Product.Common.Tests.ModelValidation.Factories;

public class ErrorFactoryTests
{
    [Fact]
    public void ProcessModelState_WhenModelStateIsValid_ReturnsEmptyCollection()
    {
        // Arrange
        var modelState = new ModelStateDictionary();
        var sut = new ErrorFactory(new Mock<IFieldProcessorFactory>().Object);

        // Act
        var result = sut.ProcessModelState(modelState);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void ProcessModelState_WhenModelStateIsInvalid_ReturnsCollectionWithErrors()
    {
        // Arrange
        var modelState = new ModelStateDictionary();
        modelState.AddModelError("key", "error");
        var fieldProcessorFactory = new Mock<IFieldProcessorFactory>();
        fieldProcessorFactory.Setup(x => x.Get(It.IsAny<Type>())).Returns(new DefaultFieldProcessor());
        var sut = new ErrorFactory(fieldProcessorFactory.Object);

        // Act
        var result = sut.ProcessModelState(modelState);

        // Assert
        Assert.NotEmpty(result);
    }

    [Fact]
    public void ProcessModelState_WhenModelStateIsInvalid_ReturnsCollectionWithErrorsWithCorrectField()
    {
        // Arrange
        var modelState = new ModelStateDictionary();
        modelState.AddModelError("key", "error");
        var processorFactory = new Mock<IFieldProcessorFactory>();
        processorFactory.Setup(x => x.Get(It.IsAny<Type>())).Returns(new DefaultFieldProcessor());
        var sut = new ErrorFactory(processorFactory.Object);

        // Act
        var result = sut.ProcessModelState(modelState);

        result.First().Code.ShouldBe("urn:au-cds:error:cds-all:Field/Invalid");
        result.First().Title.ShouldBe("Invalid Field");
        result.First().Detail.ShouldBe("error");
    }
}