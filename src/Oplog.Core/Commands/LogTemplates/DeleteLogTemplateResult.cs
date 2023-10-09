using Oplog.Core.Common;

namespace Oplog.Core.Commands.LogTemplates;

public class DeleteLogTemplateResult
{
    public string Message { get; private set; }
    public string ResultType { get; private set; }

    public DeleteLogTemplateResult LogtemplateNotFound()
    {
        ResultType = ResultTypeConstants.NotFound;
        Message = "Log template not found!";
        return this;
    }

    public DeleteLogTemplateResult LogtemplateDeleted()
    {
        ResultType = ResultTypeConstants.Success;
        Message = "log template deleted!";
        return this;
    }
}
