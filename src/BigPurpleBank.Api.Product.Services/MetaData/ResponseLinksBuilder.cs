using BigPurpleBank.Api.Product.Model;
using BigPurpleBank.Api.Product.Model.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace BigPurpleBank.Api.Product.Services.MetaData;

public class ResponseLinksBuilder : IResponseMetaDataBuilder
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ResponseLinksBuilder(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public LinksPaginated BuildLinksAsync(
        int page,
        int pageSize,
        int totalRecords)
    {
        var baseUrl = _httpContextAccessor.HttpContext.Request.GetDisplayUrl ();
        var maxPages = GetMaxPages(pageSize, totalRecords);
        return new LinksPaginated
        {
            Self = $"{baseUrl}?page={page}&page-size={pageSize}",
            First = $"{baseUrl}?page=1&page-size={pageSize}",
            Last = $"{baseUrl}?page={maxPages}&page-size={pageSize}",
            Prev = page > 1 ? $"{baseUrl}?page={page - 1}&page-size={pageSize}" : null,
            Next = page < maxPages ? $"{baseUrl}?page={page + 1}&page-size={pageSize}" : null
        };
    }

    private static int GetMaxPages(
        int pageSize,
        int totalRecords)
    {
        var maxPages = (int)Math.Ceiling((double)totalRecords / pageSize);
        return maxPages;
    }

    public MetaPaginated BuildMetaDataAsync(
        int pageSize,
        int totalRecords) =>
        new()
        {
            TotalPages = GetMaxPages(pageSize, totalRecords),
            TotalRecords = totalRecords
        };
}