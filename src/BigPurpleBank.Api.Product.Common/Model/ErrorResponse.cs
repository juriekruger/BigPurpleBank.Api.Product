using System.Text.Json;

namespace BigPurpleBank.Api.Product.Common.Model;

public class ErrorResponse
{
    public List<Error>? Errors { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}