using BigPurpleBank.Api.Product.Model;
using BigPurpleBank.Api.Product.Model.Requests;

namespace BigPurpleBank.Api.Product.Services.MetaData;

public interface IResponseMetaDataBuilder: IApiService
{
    /// <summary>
    /// Build the links for the response
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalRecords"></param>
    /// <returns></returns>
    LinksPaginated BuildLinksAsync(
        int page,
        int pageSize,
        int totalRecords);

    /// <summary>
    /// Build the meta data for the response
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="totalRecords"></param>
    /// <returns></returns>
    MetaPaginated BuildMetaDataAsync(
        int pageSize,
        int totalRecords);
}