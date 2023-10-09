namespace Oplog.Core.AzureSearch;

public interface IIndexDocumentClient
{
    Task<bool> Create(LogDocument log);
    Task<bool> Delete(string logId);
    Task<bool> Update(LogDocument log);
}