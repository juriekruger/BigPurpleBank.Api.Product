using BigPurpleBank.Api.Product.Common.ModelValidation.FieldProcessors;

namespace BigPurpleBank.Api.Product.Common.Tests.ModelValidation.FieldProcessors;

public class DefaultFieldProcessorTests
{
    [Fact]
    public void CanProcess_ReturnsTrue()
    {
        // Arrange
        var processor = new DefaultFieldProcessor();
        // Act
        var result = processor.CanProcess(typeof(string));
        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Process_WhenValueErrorsIsNull_ReturnsEmptyEnumerable()
    {
        // Arrange
        var processor = new DefaultFieldProcessor();
        // Act
        var result = processor.Process(null);
        // Assert
        Assert.Empty(result);
    }
}