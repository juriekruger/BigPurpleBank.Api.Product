using BigPurpleBank.Api.Product.Common.ModelValidation.Factories;
using BigPurpleBank.Api.Product.Common.ModelValidation.FieldProcessors;

namespace BigPurpleBank.Api.Product.Common.Tests.ModelValidation.Factories;

public class FieldProcessorFactoryTests
{
    private readonly IFieldProcessorFactory _factory;

    public FieldProcessorFactoryTests()
    {
        _factory = new FieldProcessorFactory(new List<IFieldProcessor>
        {
            new DateTimeProcessor()
        });
    }

    [Fact]
    public void Get_WhenTypeIsNull_ReturnsDefaultProcessor()
    {
        var processor = _factory.Get(null);

        Assert.IsType<DefaultFieldProcessor>(processor);
    }

    [Fact]
    public void Get_WhenTypeIsString_ReturnsStringProcessor()
    {
        var processor = _factory.Get(typeof(string));

        Assert.IsType<DefaultFieldProcessor>(processor);
    }

    [Fact]
    public void Get_WhenTypeIsInt_ReturnsIntProcessor()
    {
        var processor = _factory.Get(typeof(int));

        Assert.IsType<DefaultFieldProcessor>(processor);
    }

    [Fact]
    public void Get_WhenTypeIsDecimal_ReturnsDecimalProcessor()
    {
        var processor = _factory.Get(typeof(decimal));

        Assert.IsType<DefaultFieldProcessor>(processor);
    }

    [Fact]
    public void Get_WhenTypeIsDateTime_ReturnsDateTimeProcessor()
    {
        var processor = _factory.Get(typeof(DateTime));

        Assert.IsType<DateTimeProcessor>(processor);
    }

    [Fact]
    public void Get_WhenTypeIsGuid_ReturnsGuidProcessor()
    {
        var processor = _factory.Get(typeof(Guid));

        Assert.IsType<DefaultFieldProcessor>(processor);
    }

    [Fact]
    public void Get_WhenTypeIsBoolean_ReturnsBooleanProcessor()
    {
        var processor = _factory.Get(typeof(bool));

        Assert.IsType<DefaultFieldProcessor>(processor);
    }

    [Fact]
    public void Get_WhenTypeIsList_ReturnsListProcessor()
    {
        var processor = _factory.Get(typeof(List<string>));

        Assert.IsType<DefaultFieldProcessor>(processor);
    }

    [Fact]
    public void Get_WhenTypeIsDictionary_ReturnsDictionaryProcessor()
    {
        var processor = _factory.Get(typeof(Dictionary<string, string>));

        Assert.IsType<DefaultFieldProcessor>(processor);
    }

    [Fact]
    public void Get_WhenTypeIsObject_ReturnsObjectProcessor()
    {
        var processor = _factory.Get(typeof(object));

        Assert.IsType<DefaultFieldProcessor>(processor);
    }
}