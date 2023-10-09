using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oplog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixAreaAndUnitDataMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
@"DELETE 
FROM OperationAreaUnit
WHERE UnitsId = 1091 AND OperationAreasId = 10003;
GO"
   );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
