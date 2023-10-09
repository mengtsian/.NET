using Azure.Search.Documents.Models;

namespace Oplog.Core.AzureSearch;

public interface IIndexSearchClient
{
    Task<SearchResults<LogDocument>> GetLogDocumentsByIds(List<int> ids, List<string> sortBy);
    Task<SearchResults<LogDocument>> Search(SearchRequest searchRequest);
}