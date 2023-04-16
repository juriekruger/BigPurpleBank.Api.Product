namespace BigPurpleBank.Api.Product.Common.Model;

public class InvalidPageError : Error
{
    public InvalidPageError()
    {
        Code = "urn:au-cds:error:cds-all:Field/InvalidPage";
        Title = "Invalid Page";
        Detail = "The page number requested is not valid.";
    }
}