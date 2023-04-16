using System.Text.Json.Serialization;
using BigPurpleBank.Api.Product.Model.Enum;

namespace BigPurpleBank.Api.Product.Model.Dto;

public class ProductDto : BaseDto
{
    public int EffectiveFromUnix { get; set; }
    public int EffectiveToUnix { get; set; }
    public int LastUpdatedUnix { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
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