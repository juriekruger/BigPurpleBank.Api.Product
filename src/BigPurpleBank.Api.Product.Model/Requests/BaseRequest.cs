using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BigPurpleBank.Api.Product.Model.Requests;

public class BaseRequest
{
    
    [FromQuery(Name = "page")]
    [Range(0, int.MaxValue)]
    public int? Page { get; set; } = 1;

    [FromQuery(Name = "page-size")]
    [Range(1, 1000)]
    public int? PageSize { get; set; } = 25;
}