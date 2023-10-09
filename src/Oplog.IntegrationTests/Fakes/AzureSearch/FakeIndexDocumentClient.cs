using Oplog.Core.AzureSearch;

namespace Oplog.IntegrationTests.Fakes.AzureSearch;

public class FakeIndexDocumentClient : IIndexDocumentClient
{
    public Task<bool> Create(LogDocument log)
    {
        Task<bool> task = Task.Run(() =>
        {
            return true;
        });

        return task;
    }

    public Task<bool> Delete(string logId)
    {
        Task<bool> task = Task.Run(() =>
        {
            return true;
        });

        return task;
    }

    public Task<bool> Update(LogDocument log)
    {
        Task<bool> task = Task.Run(() =>
        {
            return true;
        });

        return task;
    }
}
