using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GtfsApi.Migrations
{
    /// <inheritdoc />
    public partial class GtfsRoutes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GtfsRoutes_Agencies_AgencyId",
                table: "GtfsRoutes");

            migrationBuilder.DropIndex(
                name: "IX_GtfsRoutes_AgencyId",
                table: "GtfsRoutes");

            migrationBuilder.AlterColumn<string>(
                name: "AgencyId",
                table: "GtfsRoutes",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "FkAgencyId",
                table: "GtfsRoutes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GtfsRoutes_FkAgencyId",
                table: "GtfsRoutes",
                column: "FkAgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_GtfsRoutes_Agencies_FkAgencyId",
                table: "GtfsRoutes",
                column: "FkAgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GtfsRoutes_Agencies_FkAgencyId",
                table: "GtfsRoutes");

            migrationBuilder.DropIndex(
                name: "IX_GtfsRoutes_FkAgencyId",
                table: "GtfsRoutes");

            migrationBuilder.DropColumn(
                name: "FkAgencyId",
                table: "GtfsRoutes");

            migrationBuilder.AlterColumn<int>(
                name: "AgencyId",
                table: "GtfsRoutes",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_GtfsRoutes_AgencyId",
                table: "GtfsRoutes",
                column: "AgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_GtfsRoutes_Agencies_AgencyId",
                table: "GtfsRoutes",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
