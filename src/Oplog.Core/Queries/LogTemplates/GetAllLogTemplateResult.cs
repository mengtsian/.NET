namespace Oplog.Core.Queries;

public class GetAllLogTemplateResult
{
    public GetAllLogTemplateResult(int id, string name, int? logTypeId, int? areaId, string text, string author, int? unit, int? subType, bool? isCritical)
    {
        Id = id;
        Name = name;
        LogTypeId = logTypeId;
        OperationAreaId = areaId;
        Text = text;
        Author = author;
        Unit = unit;
        Subtype = subType;
        IsCritical = isCritical;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public int? LogTypeId { get; set; }
    public int? OperationAreaId { get; set; }
    public string Text { get; set; }
    public string Author { get; set; }
    public int? Unit { get; set; }
    public int? Subtype { get; set; }
    public bool? IsCritical { get; set; }
}
