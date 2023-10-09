using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Microsoft.Extensions.Logging;

namespace Oplog.Core.AzureSearch;

public sealed class IndexDocumentClient : SearchClientBase, IIndexDocumentClient
{
    private const int ToTalTryCount = 5;
    private readonly ILogger<IndexDocumentClient> _logger;
    public IndexDocumentClient(SearchConfiguration configurationOptions, ILogger<IndexDocumentClient> logger) : base(configurationOptions)
    {
        _logger = logger;
    }

    public async Task<bool> Create(LogDocument log)
    {
        try
        {
            var searchClient = GetSearchClient(isAdminKey: true);

            IndexDocumentsBatch<LogDocument> batch = IndexDocumentsBatch.Create(IndexDocumentsAction.Upload(log));
            IndexDocumentsOptions options = new() { ThrowOnAnyError = true };

            var result = await searchClient.IndexDocumentsAsync(batch, options);
            var firstResult = result.Value.Results[0];
            if (firstResult.Succeeded)
            {
                bool isIndexed = await IsLogDocumentIndexed(log.Id);
                return isIndexed;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task<bool> IsLogDocumentIndexed(string logId)
    {
        //Note: This is to confirm the document is indexed
        int numberOfTries = 0;
        var searchClient = GetSearchClient(isAdminKey: true);
        while (numberOfTries < ToTalTryCount)
        {
            try
            {
                var logDocument = await searchClient.GetDocumentAsync<LogDocument>(logId);

                //Note: No need to check for null log document. GetDocumentAsync method throws an exception if the document is not found
                return true;
            }
            catch (Exception)
            {
                //Note: Ignore the exception. Just keep trying.
                numberOfTries++;
            }
        }

        return false;
    }

    public async Task<bool> Update(LogDocument log)
    {
        try
        {
            var searchClient = GetSearchClient(isAdminKey: true);
            IndexDocumentsBatch<LogDocument> batch = IndexDocumentsBatch.Create(IndexDocumentsAction.MergeOrUpload(log));
            IndexDocumentsOptions options = new() { ThrowOnAnyError = true };

            var result = await searchClient.IndexDocumentsAsync(batch, options);

            //Note: Delay the return to update the indexed document
            await Task.Delay(1000);

            var firstResult = result.Value.Results[0];
            if (firstResult.Succeeded)
            {
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> Delete(string logId)
    {
        try
        {
            var searchClient = GetSearchClient(isAdminKey: true);
            var log = await searchClient.GetDocumentAsync<LogDocument>(logId);

            IndexDocumentsBatch<LogDocument> batch = IndexDocumentsBatch.Create(
                    IndexDocumentsAction.Delete<LogDocument>(log));

            IndexDocumentsOptions options = new() { ThrowOnAnyError = true };

            var result = await searchClient.IndexDocumentsAsync(batch, options);

            var firstResult = result.Value.Results[0];
            if (firstResult.Succeeded)
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Cognitive search exception");
            //Note: Do not throw the error. Just log the error and operation should continue for multiple deletes without breaking on exception. 
            return false;
        }
    }
}
