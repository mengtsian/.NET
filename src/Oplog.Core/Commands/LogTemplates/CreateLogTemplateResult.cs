using Oplog.Core.Common;

namespace Oplog.Core.Commands.LogTemplates;

public class CreateLogTemplateResult
{
    public string Message { get; set; }
    public string ResultType { get; set; }
    public int LogTemplateId { get; set; }

    public CreateLogTemplateResult LogTemplateCreated(int id)
    {
        LogTemplateId = id;
        ResultType = ResultTypeConstants.Success;
        Message = "Log template created";
        return this;
    }
}
