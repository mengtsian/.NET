using Oplog.Core.AzureSearch;
using Oplog.Core.Infrastructure;
using Oplog.Persistence.Models;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands.Logs;

public sealed class DeleteLogsCommandHandler : ICommandHandler<DeleteLogsCommand, DeleteLogsResult>
{
    private readonly ILogsRepository _logsRepository;
    private readonly IIndexDocumentClient _documentClient;
    public DeleteLogsCommandHandler(ILogsRepository logsRepository, IIndexDocumentClient documentClient)
    {
        _logsRepository = logsRepository;
        _documentClient = documentClient;
    }

    public async Task<DeleteLogsResult> Handle(DeleteLogsCommand command)
    {
        var deleteLogsResult = new DeleteLogsResult();
        List<Log> logsToDelete = await _logsRepository.GetByIds(command.Ids);

        if (!logsToDelete.Any())
        {
            _logsRepository.DeleteBulk(logsToDelete);
            await _logsRepository.Save();
        }

        foreach (var logId in command.Ids)
        {
            await _documentClient.Delete(logId.ToString());
        }

        //Note: Cognitive search index takes time to update. Adding a delay to reflect it
        await Task.Delay(2500);

        return deleteLogsResult.AllRequestedLogsDeleted();
    }
}
