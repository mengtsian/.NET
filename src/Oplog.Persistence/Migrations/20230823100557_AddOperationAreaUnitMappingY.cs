using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oplog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOperationAreaUnitMappingY : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
@"INSERT INTO OperationAreaUnit
				(OperationAreasId,
				UnitsId)
VALUES
	(10005,1117),(10005,1118),(10005,1128),(10005,1119),(10005,1120),
    (10005,1121),(10005,1122),(10005,1123),(10005,1124),(10005,1125),
    (10005,1132),(10005,1146),(10005,1126),(10005,1127),(10005,1129),
    (10005,1133),(10005,1130),(10005,1171),(10005,1131),(10005,1150),
    (10005,1147);
GO"
  );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
