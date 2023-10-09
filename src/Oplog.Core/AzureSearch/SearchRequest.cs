namespace Oplog.Core.AzureSearch;

public class SearchRequest
{
    public SearchRequest(int[] logTypeIds, int[] areaIds, int[] subTypeIds, int[] unitIds, string searchText, DateTime fromDate, DateTime toDate, List<string> sortBy, int pageSize, int pageNumber)
    {
        LogTypeIds = logTypeIds;
        AreaIds = areaIds;
        SubTypeIds = subTypeIds;
        UnitIds = unitIds;
        SearchText = searchText;
        FromDate = fromDate;
        ToDate = toDate;
        SortBy = sortBy;
        PageSize = pageSize;
        PageNumber = pageNumber;
    }
    public int[] LogTypeIds { get; set; }
    public int[] AreaIds { get; set; }
    public int[] SubTypeIds { get; set; }
    public int[] UnitIds { get; set; }
    public string SearchText { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public List<string> SortBy { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}
