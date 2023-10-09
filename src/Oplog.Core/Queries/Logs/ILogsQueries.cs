using Oplog.Core.Queries.Logs;

namespace Oplog.Core.Queries;

public interface ILogsQueries
{
    Task<List<LogsResult>> GetAllLogs();
    Task<List<LogsResult>> GetLogsByDate(DateTime fromDate, DateTime toDate);
    Task<List<LogsResult>> GetFilteredLogs(LogsFilter filter);
}
