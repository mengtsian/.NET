using Oplog.Core.AzureSearch;
using Oplog.Core.Infrastructure;
using Oplog.Persistence.Models;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands.Logs;

public sealed class CreateLogCommandHandler : ICommandHandler<CreateLogCommand, CreateLogResult>
{
    private readonly ILogsRepository _logsRepository;
    private readonly IIndexDocumentClient _documentClient;

    public CreateLogCommandHandler(ILogsRepository logsRepository, IIndexDocumentClient documentClient)
    {
        _logsRepository = logsRepository;
        _documentClient = documentClient;
    }
    public async Task<CreateLogResult> Handle(CreateLogCommand command)
    {
        var result = new CreateLogResult();
        var newLog = new Log
        {
            LogTypeId = command.LogType,
            OperationAreaId = command.OperationsAreaId,
            Author = command.Author,
            Unit = command.Unit,
            Subtype = command.SubType,
            Text = command.Comment,
            EffectiveTime = command.EffectiveTime,
            CreatedBy = command.CreatedBy,
            CreatedDate = DateTime.Now,
            IsCritical = command.IsCritical
        };

        await _logsRepository.Insert(newLog);
        await _logsRepository.Save();

        var log = await _logsRepository.GetDetailedLogById(newLog.Id);
        bool succeeded = await _documentClient.Create(new LogDocument
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


        if (succeeded)
        {
            return result.LogCreated(newLog.Id);
        }
        else
        {
            return result.LogCreatedWithFailures(newLog.Id);
        }
    }
}
