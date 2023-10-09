using Oplog.Core.Infrastructure;

namespace Oplog.Core.Commands.Logs;

public sealed record DeleteLogsCommand : ICommand
{
    public DeleteLogsCommand(IEnumerable<int> ids)
    {
        Ids = ids.ToList();
    }

    public List<int> Ids { get; set; }
}
