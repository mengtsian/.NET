using Oplog.Core.Infrastructure;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands.LogTemplates;

public sealed class DeleteLogTemplateCommandHandler : ICommandHandler<DeleteLogTemplateCommand, DeleteLogTemplateResult>
{
    private readonly ILogTemplateRepository _templateRepository;
    public DeleteLogTemplateCommandHandler(ILogTemplateRepository templateRepository)
    {
        _templateRepository = templateRepository;
    }
    public async Task<DeleteLogTemplateResult> Handle(DeleteLogTemplateCommand command)
    {
        var result = new DeleteLogTemplateResult();
        var logtemplate = await _templateRepository.GetById(command.Id);
        if (logtemplate == null)
        {
            return result.LogtemplateNotFound();
        }
        _templateRepository.Delete(logtemplate);
        await _templateRepository.Save();
        return result.LogtemplateDeleted();
    }
}
