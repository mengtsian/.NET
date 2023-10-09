using Oplog.Core.Common;

namespace Oplog.Core.Commands.Logs;

public class UpdateLogResult
{
    public string Message { get; set; }
    public string ResultType { get; set; }

    public UpdateLogResult NotFound()
    {
        ResultType = ResultTypeConstants.NotFound;
        Message = "Log not found";
        return this;
    }
    public UpdateLogResult LogUpdated()
    {
        ResultType = ResultTypeConstants.Success;
        Message = "Log updated";
        return this;
    }
}
