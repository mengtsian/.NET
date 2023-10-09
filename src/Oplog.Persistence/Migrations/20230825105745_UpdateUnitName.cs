using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oplog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUnitName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
@"UPDATE Units
SET Name = 'TCM-Generelt'
WHERE Id = 1153;
GO"
  );

            migrationBuilder.Sql(
@"UPDATE ConfiguredTypes
SET Name = 'TCM-Generelt'
WHERE Id = 1153;
GO"
  );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
