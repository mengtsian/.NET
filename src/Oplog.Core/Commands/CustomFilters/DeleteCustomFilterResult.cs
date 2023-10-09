using Oplog.Core.Common;

namespace Oplog.Core.Commands.CustomFilters;

public class DeleteCustomFilterResult
{
    public string Message { get; set; }
    public string ResultType { get; set; }

    public DeleteCustomFilterResult CustomFilterNotFound()
    {
        ResultType = ResultTypeConstants.NotFound;
        Message = "Custom filter not found!";
        return this;
    }

    public DeleteCustomFilterResult CustomFilterDeleted()
    {
        ResultType = ResultTypeConstants.Success;
        Message = "Custom filter deleted!";
        return this;
    }

    public DeleteCustomFilterResult GlobalFilterDeleteNotAllowed()
    {
        ResultType = ResultTypeConstants.NotAllowed;
        Message = "Only admins can delete global filtyer!";
        return this;
    }
}
