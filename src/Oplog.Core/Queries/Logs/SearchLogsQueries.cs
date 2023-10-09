using Oplog.Core.AzureSearch;

namespace Oplog.Core.Queries.Logs;

public class SearchLogsQueries : ISearchLogsQueries
{
    private readonly IIndexSearchClient _indexSearchClient;
    public SearchLogsQueries(IIndexSearchClient indexSearchClient)
    {
        _indexSearchClient = indexSearchClient;
    }

    public async Task<SearchLogsResult> Search(SearchRequest searchRequest)
    {
        var result = await _indexSearchClient.Search(searchRequest);

        SearchLogsResult searchLogsResult = new()
        {
            RecordsCount = result.TotalCount
        };

        if (searchLogsResult.RecordsCount != null)
        {
            searchLogsResult.TotalPages = (int)Math.Ceiling((decimal)(searchLogsResult.RecordsCount / searchRequest.PageSize));
        }

        foreach (var item in result.GetResults())
        {
            searchLogsResult.Logs.Add(LogsResult.Map(item.Document));
        }

        return searchLogsResult;
    }

    public async Task<GetLogsByIdsSearchResult> GetLogsByIds(List<int> ids, List<string> sortBy)
    {
        var result = await _indexSearchClient.GetLogDocumentsByIds(ids, sortBy);

        GetLogsByIdsSearchResult getLogsByIdsSearchResult = new();

        foreach (var item in result.GetResults())
        {
            getLogsByIdsSearchResult.Logs.Add(LogsResult.Map(item.Document));
        }

        return getLogsByIdsSearchResult;
    }
}
