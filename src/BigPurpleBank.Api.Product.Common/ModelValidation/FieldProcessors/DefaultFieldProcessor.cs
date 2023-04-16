namespace BigPurpleBank.Api.Product.Common.ModelValidation.FieldProcessors;

public class DefaultFieldProcessor : BaseProcessor, IFieldProcessor
{
    protected override string Code => "urn:au-cds:error:cds-all:Field/Invalid";
    protected override string Title => "Invalid Field";

    public bool CanProcess(
        Type? type) =>
        true;
}