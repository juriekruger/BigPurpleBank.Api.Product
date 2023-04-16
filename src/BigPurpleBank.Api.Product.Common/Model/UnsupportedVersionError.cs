namespace BigPurpleBank.Api.Product.Common.Model;

public class UnsupportedVersionError : Error
{
    public UnsupportedVersionError()
    {
        Code = "urn:au-cds:error:cds-all:Header/UnsupportedVersion";
        Title = "Unsupported Version";
        Detail = "The version provided is not supported.";
    }
}