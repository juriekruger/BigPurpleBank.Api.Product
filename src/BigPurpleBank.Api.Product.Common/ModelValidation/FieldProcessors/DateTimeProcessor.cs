namespace BigPurpleBank.Api.Product.Common.ModelValidation.FieldProcessors;

public class DateTimeProcessor : BaseProcessor, IFieldProcessor
{
    protected override string Code => "urn:au-cds:error:cds-all:Field/InvalidDateTime";
    protected override string Title => "Invalid Date";

    public bool CanProcess(
        Type? type) =>
        type == typeof(DateTime) || type == typeof(DateTimeOffset) || type == typeof(DateTime?) || type == typeof(DateTimeOffset?);
}