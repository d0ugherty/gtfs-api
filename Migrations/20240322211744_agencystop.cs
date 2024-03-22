using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GtfsApi.Migrations
{
    /// <inheritdoc />
    public partial class agencystop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FkAgencyId",
                table: "Stops",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stops_FkAgencyId",
                table: "Stops",
                column: "FkAgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Agencies_FkAgencyId",
                table: "Stops",
                column: "FkAgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Agencies_FkAgencyId",
                table: "Stops");

            migrationBuilder.DropIndex(
                name: "IX_Stops_FkAgencyId",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "FkAgencyId",
                table: "Stops");
        }
    }
}
