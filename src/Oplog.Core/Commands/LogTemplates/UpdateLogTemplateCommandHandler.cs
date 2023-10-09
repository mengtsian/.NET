using Oplog.Core.Infrastructure;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands.LogTemplates;

public sealed class UpdateLogTemplateCommandHandler : ICommandHandler<UpdateLogTemplateCommand, UpdateLogTemplateResult>
{
    private readonly ILogTemplateRepository _logTemplateRepository;
    public UpdateLogTemplateCommandHandler(ILogTemplateRepository logTemplateRepository)
    {
        _logTemplateRepository = logTemplateRepository;
    }
    public async Task<UpdateLogTemplateResult> Handle(UpdateLogTemplateCommand command)
    {
        var logtemplate = await _logTemplateRepository.GetById(command.Id);
        var result = new UpdateLogTemplateResult();
        if (logtemplate == null)
        {
            return result.NotFound();
        }

        logtemplate.Name = command.Name;
        logtemplate.LogTypeId = command.LogTypeId;
        logtemplate.OperationAreaId = command.AreaId;
        logtemplate.Text = command.Text;
        logtemplate.Author = command.Author;
        logtemplate.Unit = command.Unit;
        logtemplate.Subtype = command.SubType;
        logtemplate.IsCritical = command.IsCritical;
        logtemplate.UpdatedBy = command.UpdatedBy;
        logtemplate.UpdatedDate = DateTime.Now;

        _logTemplateRepository.Update(logtemplate);
        await _logTemplateRepository.Save();
        return result.LogTemplateUpdated(logtemplate.Id);
    }
}
