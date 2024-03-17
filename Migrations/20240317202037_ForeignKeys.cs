using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GtfsApi.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FareAttributesEnumerable_Fares_FareId",
                table: "FareAttributesEnumerable");

            migrationBuilder.DropForeignKey(
                name: "FK_GtfsRoutes_Agencies_AgencyId",
                table: "GtfsRoutes");

            migrationBuilder.DropForeignKey(
                name: "FK_StopTimes_Stops_StopId",
                table: "StopTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_StopTimes_Trips_TripId",
                table: "StopTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_GtfsRoutes_RouteId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_StopTimes_TripId",
                table: "StopTimes");

            migrationBuilder.RenameColumn(
                name: "RouteId",
                table: "Trips",
                newName: "FkRouteId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_RouteId",
                table: "Trips",
                newName: "IX_Trips_FkRouteId");

            migrationBuilder.RenameColumn(
                name: "TripId",
                table: "StopTimes",
                newName: "GtfsStopId");

            migrationBuilder.RenameColumn(
                name: "StopId",
                table: "StopTimes",
                newName: "FkTripId");

            migrationBuilder.RenameIndex(
                name: "IX_StopTimes_StopId",
                table: "StopTimes",
                newName: "IX_StopTimes_FkTripId");

            migrationBuilder.RenameColumn(
                name: "AgencyName",
                table: "GtfsRoutes",
                newName: "GtfsAgencyId");

            migrationBuilder.RenameColumn(
                name: "AgencyId",
                table: "GtfsRoutes",
                newName: "FkAgencyId");

            migrationBuilder.RenameIndex(
                name: "IX_GtfsRoutes_AgencyId",
                table: "GtfsRoutes",
                newName: "IX_GtfsRoutes_FkAgencyId");

            migrationBuilder.RenameColumn(
                name: "FareId",
                table: "FareAttributesEnumerable",
                newName: "FkFareId");

            migrationBuilder.RenameIndex(
                name: "IX_FareAttributesEnumerable_FareId",
                table: "FareAttributesEnumerable",
                newName: "IX_FareAttributesEnumerable_FkFareId");

            migrationBuilder.AddColumn<string>(
                name: "GtfsRouteId",
                table: "Trips",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FkStopId",
                table: "StopTimes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GtfsTripId",
                table: "StopTimes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GtfsFareId",
                table: "FareAttributesEnumerable",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_StopTimes_FkStopId",
                table: "StopTimes",
                column: "FkStopId");

            migrationBuilder.AddForeignKey(
                name: "FK_FareAttributesEnumerable_Fares_FkFareId",
                table: "FareAttributesEnumerable",
                column: "FkFareId",
                principalTable: "Fares",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GtfsRoutes_Agencies_FkAgencyId",
                table: "GtfsRoutes",
                column: "FkAgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StopTimes_Stops_FkStopId",
                table: "StopTimes",
                column: "FkStopId",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StopTimes_Trips_FkTripId",
                table: "StopTimes",
                column: "FkTripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_GtfsRoutes_FkRouteId",
                table: "Trips",
                column: "FkRouteId",
                principalTable: "GtfsRoutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FareAttributesEnumerable_Fares_FkFareId",
                table: "FareAttributesEnumerable");

            migrationBuilder.DropForeignKey(
                name: "FK_GtfsRoutes_Agencies_FkAgencyId",
                table: "GtfsRoutes");

            migrationBuilder.DropForeignKey(
                name: "FK_StopTimes_Stops_FkStopId",
                table: "StopTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_StopTimes_Trips_FkTripId",
                table: "StopTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_GtfsRoutes_FkRouteId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_StopTimes_FkStopId",
                table: "StopTimes");

            migrationBuilder.DropColumn(
                name: "GtfsRouteId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "FkStopId",
                table: "StopTimes");

            migrationBuilder.DropColumn(
                name: "GtfsTripId",
                table: "StopTimes");

            migrationBuilder.DropColumn(
                name: "GtfsFareId",
                table: "FareAttributesEnumerable");

            migrationBuilder.RenameColumn(
                name: "FkRouteId",
                table: "Trips",
                newName: "RouteId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_FkRouteId",
                table: "Trips",
                newName: "IX_Trips_RouteId");

            migrationBuilder.RenameColumn(
                name: "GtfsStopId",
                table: "StopTimes",
                newName: "TripId");

            migrationBuilder.RenameColumn(
                name: "FkTripId",
                table: "StopTimes",
                newName: "StopId");

            migrationBuilder.RenameIndex(
                name: "IX_StopTimes_FkTripId",
                table: "StopTimes",
                newName: "IX_StopTimes_StopId");

            migrationBuilder.RenameColumn(
                name: "GtfsAgencyId",
                table: "GtfsRoutes",
                newName: "AgencyName");

            migrationBuilder.RenameColumn(
                name: "FkAgencyId",
                table: "GtfsRoutes",
                newName: "AgencyId");

            migrationBuilder.RenameIndex(
                name: "IX_GtfsRoutes_FkAgencyId",
                table: "GtfsRoutes",
                newName: "IX_GtfsRoutes_AgencyId");

            migrationBuilder.RenameColumn(
                name: "FkFareId",
                table: "FareAttributesEnumerable",
                newName: "FareId");

            migrationBuilder.RenameIndex(
                name: "IX_FareAttributesEnumerable_FkFareId",
                table: "FareAttributesEnumerable",
                newName: "IX_FareAttributesEnumerable_FareId");

            migrationBuilder.CreateIndex(
                name: "IX_StopTimes_TripId",
                table: "StopTimes",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_FareAttributesEnumerable_Fares_FareId",
                table: "FareAttributesEnumerable",
                column: "FareId",
                principalTable: "Fares",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GtfsRoutes_Agencies_AgencyId",
                table: "GtfsRoutes",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StopTimes_Stops_StopId",
                table: "StopTimes",
                column: "StopId",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StopTimes_Trips_TripId",
                table: "StopTimes",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_GtfsRoutes_RouteId",
                table: "Trips",
                column: "RouteId",
                principalTable: "GtfsRoutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
