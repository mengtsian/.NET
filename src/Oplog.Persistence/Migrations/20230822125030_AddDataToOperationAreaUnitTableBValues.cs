using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oplog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToOperationAreaUnitTableBValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
@"INSERT INTO OperationAreaUnit
				(OperationAreasId,
				UnitsId)
VALUES
	(10002,1097),(10002,1098),(10002,1113),
    (10003,1091),(10003,1100),(10003,1110),(10003,1152),(10003,1111),(10003,1112),(10003,1114),(10003,1115),(10003,1141),
    (10004,1095),(10004,1096),(10004,1102),(10004,1103),(10004,1104),(10004,1105),(10004,1106),(10004,1107),(10004,1108),(10004,1109),
    (10004,1134),(10004,1135),(10004,1099),(10004,1172);
GO"
   );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
