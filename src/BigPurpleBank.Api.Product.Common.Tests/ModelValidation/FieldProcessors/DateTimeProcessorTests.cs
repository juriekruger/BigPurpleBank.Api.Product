using BigPurpleBank.Api.Product.Common.ModelValidation.FieldProcessors;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BigPurpleBank.Api.Product.Common.Tests.ModelValidation.FieldProcessors;

public class DateTimeProcessorTests
{
    private readonly DateTimeProcessor _processor;

    public DateTimeProcessorTests()
    {
        _processor = new DateTimeProcessor();
    }

    [Fact]
    public void CanProcess_WhenTypeIsDateTime_ReturnsTrue()
    {
        var result = _processor.CanProcess(typeof(DateTime));

        Assert.True(result);
    }

    [Fact]
    public void CanProcess_WhenTypeIsNotDateTime_ReturnsFalse()
    {
        var result = _processor.CanProcess(typeof(string));

        Assert.False(result);
    }

    [Fact]
    public void Process_WhenValueErrorsIsNull_ReturnsEmptyEnumerable()
    {
        var result = _processor.Process(null);

        Assert.Empty(result);
    }

    [Fact]
    public void Process_WhenValueErrorsIsEmpty_ReturnsEmptyEnumerable()
    {
        var result = _processor.Process(new ModelErrorCollection());

        Assert.Empty(result);
    }

    [Fact]
    public void Process_WhenValueErrorsContainsInvalidDateTime_ReturnsError()
    {
        var valueErrors = new ModelErrorCollection
        {
            new ModelError("The value '2021-01-01T00:00:00.0000000+10:00' is not valid for DateTime.")
        };


        var result = _processor.Process(valueErrors);

        Assert.Single(result);
        Assert.Equal("urn:au-cds:error:cds-all:Field/InvalidDateTime", result.First().Code);
        Assert.Equal("Invalid Date", result.First().Title);
        Assert.Equal("The value '2021-01-01T00:00:00.0000000+10:00' is not valid for DateTime.", result.First().Detail);
    }

    [Fact]
    public void Process_WhenValueErrorsContainsInvalidDateTimeAndInvalidDateTimeOffset_ReturnsError()
    {
        var valueErrors = new ModelErrorCollection
        {
            new ModelError("The value '2021-01-01T00:00:00.0000000+10:00' is not valid for DateTime."),
            new ModelError("The value '2021-01-01T00:00:00.0000000+10:00' is not valid for DateTimeOffset.")
        };

        var result = _processor.Process(valueErrors);

        Assert.Equal(2, result.Count());
        Assert.Equal("urn:au-cds:error:cds-all:Field/InvalidDateTime", result.First().Code);
        Assert.Equal("Invalid Date", result.First().Title);
    }
}