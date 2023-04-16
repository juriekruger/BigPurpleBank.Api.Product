namespace BigPurpleBank.Api.Product.Model;

public class ProductAdditionalInformation
{
    public string? OverviewUri { get; set; }
    public string? TermsUri { get; set; }
    public string? EligibilityUri { get; set; }
    public string? FeesAndPricingUri { get; set; }
    public string? BundleUri { get; set; }
    public ICollection<AdditionalInformationUri>? AdditionalOverviewUris { get; set; }
    public ICollection<AdditionalInformationUri>? AdditionalTermsUris { get; set; }
    public ICollection<AdditionalInformationUri>? AdditionalEligibilityUris { get; set; }
    public ICollection<AdditionalInformationUri>? AdditionalFeesAndPricingUris { get; set; }
    public ICollection<AdditionalInformationUri>? AdditionalBundleUris { get; set; }
}