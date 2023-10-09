namespace Oplog.Core.AzureSearch;

public class LogDocument
{
    public string Id { get; set; }
    public int? LogTypeId { get; set; }
    public int? ParentId { get; set; }
    public int? LastChangeUserId { get; set; }
    public DateTime? LastChangeDateTime { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public int? CreatedById { get; set; }
    public string Author { get; set; }
    public int? ScheduleItemState { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string Text { get; set; }
    public int? OperationAreaId { get; set; }
    public DateTime? EffectiveTime { get; set; }
    public int? Unit { get; set; }
    public int? Subtype { get; set; }
    public bool? IsCritical { get; set; }
    public string AreaName { get; set; }
    public string LogTypeName { get; set; }
    public string SubTypeName { get; set; }
    public string UnitName { get; set; }
}
