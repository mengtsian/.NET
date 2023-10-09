using Oplog.Core.Common;

namespace Oplog.Core.Commands.CustomFilters;

public class CreateCustomFilterResult
{
    public string Message { get; set; }
    public string ResultType { get; set; }
    public int FilterId { get; set; }
    public CreateCustomFilterResult CustomFilterCreated(int filterId)
    {
        ResultType = ResultTypeConstants.Success;
        Message = "Custom/global filter created!";
        FilterId = filterId;
        return this;
    }

    public CreateCustomFilterResult GlobalFiltercCreatedNotAllowed()
    {
        ResultType = ResultTypeConstants.NotAllowed;
        Message = "Only admins can create global filters!";
        return this;
    }
}
