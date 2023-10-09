using Oplog.Core.Infrastructure;

namespace Oplog.Core.Commands.CustomFilters;

public sealed record CreateCustomFilterCommand(string Name, string CreatedBy, bool? IsGlobalFilter, string SearchText, bool IsAdmin, List<CreateCustomFilterItem> FilterItems) : ICommand;
