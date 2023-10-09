using Azure;
using Azure.Search.Documents;

namespace Oplog.Core.AzureSearch;

public abstract class SearchClientBase
{
    private readonly SearchConfiguration searchConfiguration;
    public SearchClientBase(SearchConfiguration searchConfiguration)
    {
        this.searchConfiguration = searchConfiguration;
    }

    protected SearchClient GetSearchClient(bool isAdminKey)
    {
        string key = isAdminKey ? searchConfiguration.AdminKey : searchConfiguration.QueryKey;
        var credentials = new AzureKeyCredential(key);
        var searchClient = new SearchClient(new Uri(searchConfiguration.Endpoint), searchConfiguration.SearchIndexName, credentials);
        return searchClient;
    }
}
