using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oplog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddViewForLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW LogsView
                                    AS
                                    SELECT        l.Id, l.LogTypeId, l.ParentId, l.LastChangeUserId, l.LastChangeDateTime, l.UpdatedBy, l.UpdatedDate, l.CreatedById, l.Author, l.ScheduleItemState, l.CreatedBy, l.CreatedDate, l.Text, l.OperationAreaId, l.EffectiveTime, l.Unit, 
                                    l.Subtype, l.IsCritical, oa.Name AS AreaName, logtypes.Name AS LogTypeName, subtypes.Name AS SubTypeName, units.Name AS UnitName
                                    FROM            dbo.Logs AS l LEFT OUTER JOIN
                                    dbo.OperationAreas AS oa ON l.OperationAreaId = oa.Id LEFT OUTER JOIN
                                    dbo.ConfiguredTypes AS logtypes ON l.LogTypeId = logtypes.Id LEFT OUTER JOIN
                                    dbo.ConfiguredTypes AS subtypes ON l.Subtype = subtypes.Id LEFT OUTER JOIN
                                    dbo.ConfiguredTypes AS units ON l.Unit = units.Id;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW LogsView;");
        }
    }
}
