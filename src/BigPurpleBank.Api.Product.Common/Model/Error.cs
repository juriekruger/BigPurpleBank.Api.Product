namespace BigPurpleBank.Api.Product.Common.Model;

public class Error
{
    public string? Code { get; set; }
    public string? Title { get; set; }
    public string? Detail { get; set; }
    public ErrorMeta? Meta { get; set; }
}

public class ErrorMeta
{
    public string? Urn { get; set; }
}