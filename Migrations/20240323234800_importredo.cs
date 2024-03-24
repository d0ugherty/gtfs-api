using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GtfsApi.Migrations
{
    /// <inheritdoc />
    public partial class importredo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agencies_Modes_FkModeId",
                table: "Agencies");

            migrationBuilder.DropIndex(
                name: "IX_Agencies_FkModeId",
                table: "Agencies");

            migrationBuilder.DropColumn(
                name: "FkModeId",
                table: "Agencies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FkModeId",
                table: "Agencies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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
    }
}
