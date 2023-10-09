namespace Oplog.Core.Queries;

public class ConfiguredTypesByCategoryResult
{
    public ConfiguredTypesByCategoryResult(int id, string name, string description, int? categoryId)
    {
        Id = id;
        Name = name;
        Description = description;
        CategoryId = categoryId;

    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? CategoryId { get; set; }
}
