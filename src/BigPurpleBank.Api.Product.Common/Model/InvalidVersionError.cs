namespace BigPurpleBank.Api.Product.Common.Model;

public class InvalidVersionError : Error
{
    public InvalidVersionError()
    {
        Code = "urn:au-cds:error:cds-all:Header/InvalidVersion";
        Title = "Invalid Version";
        Detail = "The version provided is not valid.";

    }
}