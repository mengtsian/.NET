using Oplog.Core.Common;

namespace Oplog.Core.Commands.LogTemplates;

public class UpdateLogTemplateResult
{
    public string Message { get; set; }
    public string ResultType { get; set; }
    public int LogTemplateId { get; set; }

    public UpdateLogTemplateResult NotFound()
    {
        ResultType = ResultTypeConstants.NotFound;
        Message = "Log not found";
        return this;
    }
    public UpdateLogTemplateResult LogTemplateUpdated(int logTemplateId)
    {
        LogTemplateId = logTemplateId;
        ResultType = ResultTypeConstants.Success;
        Message = "Log updated";
        return this;
    }
}
