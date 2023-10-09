namespace Oplog.Core.Queries.Logs;

public class SearchLogsResult
{
    public SearchLogsResult()
    {
        Logs = new();
    }
    public long? RecordsCount { get; set; }
    public int TotalPages { get; set; }
    public List<LogsResult> Logs { get; set; }
}
