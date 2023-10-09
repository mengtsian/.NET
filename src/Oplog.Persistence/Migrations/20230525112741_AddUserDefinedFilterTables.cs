using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oplog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserDefinedFilterTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDefinedFilters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDefinedFilters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDefinedFilterItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserDefinedFilterId = table.Column<int>(type: "int", nullable: false),
                    FilterId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDefinedFilterItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDefinedFilterItems_UserDefinedFilters_UserDefinedFilterId",
                        column: x => x.UserDefinedFilterId,
                        principalTable: "UserDefinedFilters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDefinedFilterItems_UserDefinedFilterId",
                table: "UserDefinedFilterItems",
                column: "UserDefinedFilterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDefinedFilterItems");

            migrationBuilder.DropTable(
                name: "UserDefinedFilters");
        }
    }
}
