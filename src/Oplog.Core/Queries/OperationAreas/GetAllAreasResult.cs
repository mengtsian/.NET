namespace Oplog.Core.Queries;

public class GetAllAreasResult
{
    public GetAllAreasResult(int id, string name, string description, List<UnitResult> units)
    {
        Id = id;
        Name = name;
        Description = description;
        Units = units;
    }

    public GetAllAreasResult(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<UnitResult> Units { get; set; }
}

public class UnitResult
{
    public UnitResult(int id, string name, string description, int categoryId)
    {
        Id = id;
        Name = name;
        Description = description;
        CategoryId = categoryId;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
}
