namespace Oplog.Core.Queries;

public class GetGlobalCustomFiltersResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsGlobalFilter { get; set; }
    public string SearchText { get; set; }
    public List<CustomFilterItemsResult> Filters { get; set; }
}
