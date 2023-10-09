namespace Oplog.Core.Queries.Logs;

public class GetLogsByIdsSearchResult
{
    public GetLogsByIdsSearchResult()
    {
        Logs = new List<LogsResult>();
    }

    public List<LogsResult> Logs { get; set; }
}
