using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GtfsApi.Migrations
{
    /// <inheritdoc />
    public partial class modes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AgencyName",
                table: "Stops",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FkModeId",
                table: "Agencies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Modes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agencies_FkModeId",
                table: "Agencies",
                column: "FkModeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agencies_Modes_FkModeId",
                table: "Agencies",
                column: "FkModeId",
                principalTable: "Modes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agencies_Modes_FkModeId",
                table: "Agencies");

            migrationBuilder.DropTable(
                name: "Modes");

            migrationBuilder.DropIndex(
                name: "IX_Agencies_FkModeId",
                table: "Agencies");

            migrationBuilder.DropColumn(
                name: "AgencyName",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "FkModeId",
                table: "Agencies");
        }
    }
}
