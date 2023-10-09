using Oplog.Core.AzureSearch;
using Oplog.Core.Infrastructure;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands.Logs;

public sealed class UpdateLogCommandHandler : ICommandHandler<UpdateLogCommand, UpdateLogResult>
{
    private readonly ILogsRepository _logsRepository;
    private readonly IIndexDocumentClient _documentClient;

    public UpdateLogCommandHandler(ILogsRepository logsRepository, IIndexDocumentClient documentClient)
    {
        _logsRepository = logsRepository;
        _documentClient = documentClient;
    }
    public async Task<UpdateLogResult> Handle(UpdateLogCommand command)
    {
        var result = new UpdateLogResult();
        var logToUpdate = await _logsRepository.Get(command.Id);

        if (logToUpdate == null)
        {
            return result.NotFound();
        }

        logToUpdate.LogTypeId = command.LogType;
        logToUpdate.OperationAreaId = command.OperationsAreaId;
        logToUpdate.Author = command.Author;
        logToUpdate.Unit = command.Unit;
        logToUpdate.Subtype = command.SubType;
        logToUpdate.Text = command.Comment;
        logToUpdate.EffectiveTime = command.EffectiveTime;
        logToUpdate.UpdatedDate = DateTime.Now;
        logToUpdate.UpdatedBy = command.UpdatedBy;
        logToUpdate.IsCritical = command.IsCritical;

        _logsRepository.Update(logToUpdate);
        await _logsRepository.Save();

        var log = await _logsRepository.GetDetailedLogById(logToUpdate.Id);
        if (log != null)
        {
            await _documentClient.Update(new LogDocument
            {
                Id = log.Id.ToString(),
                LogTypeId = log.LogTypeId,
                UpdatedBy = log.UpdatedBy,
                UpdatedDate = log.UpdatedDate,
                CreatedBy = log.CreatedBy,
                Author = log.Author,
                CreatedDate = log.CreatedDate,
                Text = log.Text,
                OperationAreaId = log.OperationAreaId,
                EffectiveTime = log.EffectiveTime,
                Unit = log.Unit,
                Subtype = log.Subtype,
                IsCritical = log.IsCritical,
                AreaName = log.AreaName,
                LogTypeName = log.LogTypeName,
                SubTypeName = log.SubTypeName,
                UnitName = log.UnitName,
            });
        }

        return result.LogUpdated();
    }
}
