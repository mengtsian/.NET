using Oplog.Core.Infrastructure;

namespace Oplog.Core.Commands.CustomFilters;

public sealed record DeleteCustomFilterCommand(int FilterId, bool IsAdmin) : ICommand;
