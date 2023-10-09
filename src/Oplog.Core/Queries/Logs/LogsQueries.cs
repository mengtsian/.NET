using Oplog.Core.Queries.Logs;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Queries;

public class LogsQueries : ILogsQueries
{
    private readonly ILogsRepository _logsRepository;
    public LogsQueries(ILogsRepository logsRepository)
    {
        _logsRepository = logsRepository;
    }

    public async Task<List<LogsResult>> GetAllLogs()
    {
        var logs = await _logsRepository.GetAll();

        if (logs == null)
        {
            return null;
        }

        var result = new List<LogsResult>();
        foreach (var item in logs)
        {
            result.Add(LogsResult.Map(item));
        }

        return result;
    }

    public async Task<List<LogsResult>> GetLogsByDate(DateTime fromDate, DateTime toDate)
    {
        var logs = await _logsRepository.GetLogsBydate(fromDate, toDate);

        if (logs == null)
        {
            return null;
        }

        var result = new List<LogsResult>();
        foreach (var item in logs)
        {
            result.Add(LogsResult.Map(item));
        }

        return result;
    }

    public async Task<List<LogsResult>> GetFilteredLogs(LogsFilter filter)
    {
        var logs = await _logsRepository.GetFilteredLogs(filter.LogTypeIds, filter.AreaIds, filter.SubTypeIds, filter.UnitIds, filter.SearchText, filter.FromDate, filter.ToDate, filter.SortField, filter.SortDirection);

        if (logs == null)
        {
            return null;
        }

        var result = new List<LogsResult>();
        foreach (var item in logs)
        {
            result.Add(LogsResult.Map(item));
        }

        return result;
    }
}
