using BigPurpleBank.Api.Product.Model.Enum;

namespace BigPurpleBank.Api.Product.Model.Responses.Product;

public class ProductViewModel
{
    public string? ProductId { get; set; }
    public DateTime? EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }
    public DateTime? LastUpdated { get; set; }
    public ProductCategory? ProductCategory { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Brand { get; set; }
    public string? BrandName { get; set; }
    public string? ApplicationUri { get; set; }
    public bool? IsTailored { get; set; }
    public ProductAdditionalInformation? AdditionalInformation { get; set; }
    public ICollection<CardArt>? CardArt { get; set; }
    public string? Title { get; set; }
    public string? ImageUri { get; set; }
}