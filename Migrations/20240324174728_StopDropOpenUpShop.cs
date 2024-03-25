using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GtfsApi.Migrations
{
    /// <inheritdoc />
    public partial class StopDropOpenUpShop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Agencies_Fk_agencyId",
                table: "Stops");

            migrationBuilder.DropIndex(
                name: "IX_Stops_Fk_agencyId",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "AgencyName",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "Fk_agencyId",
                table: "Stops");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AgencyName",
                table: "Stops",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Fk_agencyId",
                table: "Stops",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stops_Fk_agencyId",
                table: "Stops",
                column: "Fk_agencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Agencies_Fk_agencyId",
                table: "Stops",
                column: "Fk_agencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
