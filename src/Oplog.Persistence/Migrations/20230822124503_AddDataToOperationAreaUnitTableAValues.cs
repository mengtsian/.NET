using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oplog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToOperationAreaUnitTableAValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
@"INSERT INTO OperationAreaUnit
				(OperationAreasId,
				UnitsId)
VALUES
	(10000,1094),(10001,1089),(10001,1090),(10001,1091),(10001,1092),(10001,1093),(10001,1116),(10001,1166);
GO"
    );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
