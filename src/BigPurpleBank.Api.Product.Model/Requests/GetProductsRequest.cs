using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BigPurpleBank.Api.Product.Model.Enum;
using Microsoft.AspNetCore.Mvc;

namespace BigPurpleBank.Api.Product.Model.Requests;

public class GetProductsRequest
{
    [FromQuery(Name = "effective")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EffectiveFrom? Effective { get; set; } = EffectiveFrom.Current;

    [FromQuery(Name = "updated-since")]
    public DateTime? UpdatedSince { get; set; }

    [FromQuery(Name = "brand")]
    public string? Brand { get; set; }

    [FromQuery(Name = "product-category")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProductCategory? ProductCategory { get; set; }

    [FromQuery(Name = "page")]
    [Range(0, int.MaxValue)]
    public int? Page { get; set; } = 1;

    [FromQuery(Name = "page-size")]
    [Range(1, 1000)]
    public int? PageSize { get; set; } = 25;
}