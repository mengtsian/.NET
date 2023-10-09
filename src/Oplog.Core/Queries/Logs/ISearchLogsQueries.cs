using Oplog.Core.AzureSearch;

namespace Oplog.Core.Queries.Logs;

public interface ISearchLogsQueries
{
    Task<GetLogsByIdsSearchResult> GetLogsByIds(List<int> ids, List<string> sortBy);
    Task<SearchLogsResult> Search(SearchRequest searchRequest);
}