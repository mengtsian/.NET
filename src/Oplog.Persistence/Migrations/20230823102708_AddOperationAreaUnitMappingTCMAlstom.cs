using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oplog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOperationAreaUnitMappingTCMAlstom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
@"INSERT INTO OperationAreaUnit
				(OperationAreasId,
				UnitsId)
VALUES	
    (10021,1153);
GO"
 );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
