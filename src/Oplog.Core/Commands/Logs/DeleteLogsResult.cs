using Oplog.Core.Common;

namespace Oplog.Core.Commands.Logs;

public class DeleteLogsResult
{
    public string Message { get; private set; }
    public string ResultType { get; private set; }

    public DeleteLogsResult AllRequestedLogsDeleted()
    {
        ResultType = ResultTypeConstants.Success;
        Message = "All requested logs deleted!";
        return this;
    }
}
