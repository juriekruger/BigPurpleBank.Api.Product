using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BigPurpleBank.Api.Product.Model.Enum;
using Microsoft.AspNetCore.Mvc;

namespace BigPurpleBank.Api.Product.Model.Requests;

public class GetProductsRequest: BaseRequest
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

}