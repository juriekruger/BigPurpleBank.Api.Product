using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BigPurpleBank.Api.Product.Model.Dto;

public abstract  class BaseDto
{
    [JsonProperty(PropertyName = "id")]
    public virtual string Id { get; set; }

}