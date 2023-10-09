using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oplog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToOperationAreaUnitTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
@"INSERT INTO OperationAreaUnit
				(OperationAreasId,
				UnitsId)
VALUES
	(10000,1085),(10000,1086),(10000,1087),(10000,1088);
GO"
                );        
    }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
