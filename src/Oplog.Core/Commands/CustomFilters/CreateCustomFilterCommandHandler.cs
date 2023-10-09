using Oplog.Core.Infrastructure;
using Oplog.Persistence.Models;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands.CustomFilters;

public sealed class CreateCustomFilterCommandHandler : ICommandHandler<CreateCustomFilterCommand, CreateCustomFilterResult>
{
    private readonly ICustomFilterRepository _customFilterRepository;
    public CreateCustomFilterCommandHandler(ICustomFilterRepository customFilterRepository)
    {
        _customFilterRepository = customFilterRepository;
    }
    public async Task<CreateCustomFilterResult> Handle(CreateCustomFilterCommand command)
    {
        var result = new CreateCustomFilterResult();

        var customFilterItems = new List<CustomFilterItem>();
        if (command.FilterItems.Any())
        {
            foreach (var item in command.FilterItems)
            {
                customFilterItems.Add(new CustomFilterItem()
                {
                    FilterId = item.FilterId,
                    CategoryId = item.CategoryId,
                });
            }
        }

        var isGlobalFilter = command.IsGlobalFilter != null && command.IsGlobalFilter.Value;
        if (isGlobalFilter && !command.IsAdmin)
        {
            return result.GlobalFiltercCreatedNotAllowed();
        }
        var customFilter = new CustomFilter { Name = command.Name, CreatedBy = command.CreatedBy, IsGlobalFilter = isGlobalFilter, SearchText = command.SearchText, CustomFilterItems = customFilterItems };
        await _customFilterRepository.Insert(customFilter);
        await _customFilterRepository.Save();
        return result.CustomFilterCreated(customFilter.Id);
    }
}
