namespace Oplog.Core.Queries;

public class LogsFilter
{
    public LogsFilter(int[] logTypeIds, int[] areaIds, int[] subTypeIds, int[] unitIds, string searchText, DateTime fromDate, DateTime toDate, string sortField, string sortDirection)
    {
        LogTypeIds = logTypeIds;
        AreaIds = areaIds;
        SubTypeIds = subTypeIds;
        UnitIds = unitIds;
        SearchText = searchText;
        FromDate = fromDate;
        ToDate = toDate;
        SortField = sortField;
        SortDirection = sortDirection;
    }
    public DateTime FromDate { get; set; }
    public string SearchText { get; set; }
    public DateTime ToDate { get; set; }
    public int[] LogTypeIds { get; set; }
    public int[] AreaIds { get; set; }
    public int[] SubTypeIds { get; set; }
    public int[] UnitIds { get; set; }
    public string SortField { get; set; }
    public string SortDirection { get; set; }
}
