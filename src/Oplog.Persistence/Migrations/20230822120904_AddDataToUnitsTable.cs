using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oplog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToUnitsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
@"INSERT INTO Units 
				(Id,
				[Name],
				[Description],
				CategoryId,
				IsActive)
SELECT Id,
		[Name],
		[Description],
		3,
		1
FROM ConfiguredTypes
WHERE CategoryId = 3;
GO"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
