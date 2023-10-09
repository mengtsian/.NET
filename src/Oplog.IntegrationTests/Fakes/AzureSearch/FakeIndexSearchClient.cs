using Azure.Search.Documents.Models;
using Oplog.Core.AzureSearch;

namespace Oplog.IntegrationTests.Fakes.AzureSearch;

public class FakeIndexSearchClient : IIndexSearchClient
{
    public Task<SearchResults<LogDocument>> GetLogDocumentsByIds(List<int> ids, List<string> sortBy)
    {
        throw new NotImplementedException();
    }

    public Task<SearchResults<LogDocument>> Search(SearchRequest searchRequest)
    {
        throw new NotImplementedException();
    }
}
