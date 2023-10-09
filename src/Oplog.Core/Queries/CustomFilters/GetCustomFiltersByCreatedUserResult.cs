namespace Oplog.Core.Queries;

public class GetCustomFiltersByCreatedUserResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SearchText { get; set; }
    public List<CustomFilterItemsResult> Filters { get; set; }
}
