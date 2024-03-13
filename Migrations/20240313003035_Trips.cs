using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GtfsApi.Migrations
{
    /// <inheritdoc />
    public partial class Trips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_GtfsRoutes_GtfsRouteId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_GtfsRouteId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "GtfsRouteId",
                table: "Trips");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_RouteId",
                table: "Trips",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_GtfsRoutes_RouteId",
                table: "Trips",
                column: "RouteId",
                principalTable: "GtfsRoutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_GtfsRoutes_RouteId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_RouteId",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "GtfsRouteId",
                table: "Trips",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_GtfsRouteId",
                table: "Trips",
                column: "GtfsRouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_GtfsRoutes_GtfsRouteId",
                table: "Trips",
                column: "GtfsRouteId",
                principalTable: "GtfsRoutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
