using Oplog.Core.Infrastructure;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands.CustomFilters;

public sealed class DeleteCustomFilterCommandHandler : ICommandHandler<DeleteCustomFilterCommand, DeleteCustomFilterResult>
{
    private readonly ICustomFilterRepository _customFilterRepository;
    public DeleteCustomFilterCommandHandler(ICustomFilterRepository customFilterRepository)
    {
        _customFilterRepository = customFilterRepository;
    }

    public async Task<DeleteCustomFilterResult> Handle(DeleteCustomFilterCommand command)
    {
        var customFilter = await _customFilterRepository.GetById(command.FilterId);

        var result = new DeleteCustomFilterResult();
        if (customFilter == null)
        {
            return result.CustomFilterNotFound();
        }

        if (customFilter.IsGlobalFilter && !command.IsAdmin)
        {
            return result.GlobalFilterDeleteNotAllowed();
        }

        _customFilterRepository.Delete(customFilter);
        await _customFilterRepository.Save();
        return result.CustomFilterDeleted();
    }
}