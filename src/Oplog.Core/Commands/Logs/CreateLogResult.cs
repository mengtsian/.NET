using Oplog.Core.Common;

namespace Oplog.Core.Commands.Logs;

public class CreateLogResult
{
    public int LogId { get; set; }
    public string Message { get; set; }
    public string ResultType { get; set; }

    public CreateLogResult LogCreated(int logId)
    {
        ResultType = ResultTypeConstants.Success;
        Message = "New log created";
        LogId = logId;
        return this;
    }

    public CreateLogResult LogCreatedWithFailures(int logId)
    {
        ResultType = ResultTypeConstants.Failed;
        Message = "New log created, but failed the data to index in search";
        LogId = logId;
        return this;
    }
}
