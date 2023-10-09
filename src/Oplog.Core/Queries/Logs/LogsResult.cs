using Oplog.Core.AzureSearch;
using Oplog.Persistence.Models;

namespace Oplog.Core.Queries.Logs;

public sealed class LogsResult
{
    public int Id { get; set; }
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

    public static LogsResult Map(LogsView log)
    {
        return new LogsResult
        {
            Id = log.Id,
            LogTypeId = log.LogTypeId,
            ParentId = log.ParentId,
            LastChangeUserId = log.LastChangeUserId,
            LastChangeDateTime = log.LastChangeDateTime,
            UpdatedBy = log.UpdatedBy,
            UpdatedDate = log.UpdatedDate,
            CreatedById = log.CreatedById,
            Author = log.Author,
            ScheduleItemState = log.ScheduleItemState,
            CreatedBy = log.CreatedBy,
            CreatedDate = log.CreatedDate,
            Text = log.Text,
            OperationAreaId = log.OperationAreaId,
            EffectiveTime = log.EffectiveTime,
            Unit = log.Unit,
            Subtype = log.Subtype,
            IsCritical = log.IsCritical,
            LogTypeName = log.LogTypeName,
            SubTypeName = log.SubTypeName,
            UnitName = log.UnitName,
            AreaName = log.AreaName,
        };
    }

    public static LogsResult Map(LogDocument logDocument)
    {
        return new LogsResult
        {
            Id = int.Parse(logDocument.Id),
            LogTypeId = logDocument.LogTypeId,
            ParentId = logDocument.ParentId,
            LastChangeUserId = logDocument.LastChangeUserId,
            LastChangeDateTime = logDocument.LastChangeDateTime,
            UpdatedBy = logDocument.UpdatedBy,
            UpdatedDate = logDocument.UpdatedDate,
            CreatedById = logDocument.CreatedById,
            Author = logDocument.Author,
            ScheduleItemState = logDocument.ScheduleItemState,
            CreatedBy = logDocument.CreatedBy,
            CreatedDate = logDocument.CreatedDate,
            Text = logDocument.Text,
            OperationAreaId = logDocument.OperationAreaId,
            EffectiveTime = logDocument.EffectiveTime,
            Unit = logDocument.Unit,
            Subtype = logDocument.Subtype,
            IsCritical = logDocument.IsCritical,
            LogTypeName = logDocument.LogTypeName,
            SubTypeName = logDocument.SubTypeName,
            UnitName = logDocument.UnitName,
            AreaName = logDocument.AreaName,
        };
    }
}
