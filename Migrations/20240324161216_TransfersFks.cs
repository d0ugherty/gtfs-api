using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GtfsApi.Migrations
{
    /// <inheritdoc />
    public partial class TransfersFks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agencies_ParentAgencies_FkParentAgencyId",
                table: "Agencies");

            migrationBuilder.DropForeignKey(
                name: "FK_FareAttributesTbl_Fares_FkFareId",
                table: "FareAttributesTbl");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedInfoTbl_Agencies_FkAgencyId",
                table: "FeedInfoTbl");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Agencies_FkAgencyId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Agencies_FkAgencyId",
                table: "Stops");

            migrationBuilder.DropForeignKey(
                name: "FK_StopTimes_Stops_FkStopId",
                table: "StopTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_StopTimes_Trips_FkTripId",
                table: "StopTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Stops_FromStopId1",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Stops_ToStopId1",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Routes_FkRouteId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "FkFromStopId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "FkToStopId",
                table: "Transfers");

            migrationBuilder.RenameColumn(
                name: "FkRouteId",
                table: "Trips",
                newName: "Fk_routeId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_FkRouteId",
                table: "Trips",
                newName: "IX_Trips_Fk_routeId");

            migrationBuilder.RenameColumn(
                name: "ToStopId1",
                table: "Transfers",
                newName: "Fk_toStopId");

            migrationBuilder.RenameColumn(
                name: "FromStopId1",
                table: "Transfers",
                newName: "Fk_fromStopId");

            migrationBuilder.RenameIndex(
                name: "IX_Transfers_ToStopId1",
                table: "Transfers",
                newName: "IX_Transfers_Fk_toStopId");

            migrationBuilder.RenameIndex(
                name: "IX_Transfers_FromStopId1",
                table: "Transfers",
                newName: "IX_Transfers_Fk_fromStopId");

            migrationBuilder.RenameColumn(
                name: "GtfsTripId",
                table: "StopTimes",
                newName: "TripId");

            migrationBuilder.RenameColumn(
                name: "GtfsStopId",
                table: "StopTimes",
                newName: "StopId");

            migrationBuilder.RenameColumn(
                name: "FkTripId",
                table: "StopTimes",
                newName: "Fk_tripId");

            migrationBuilder.RenameColumn(
                name: "FkStopId",
                table: "StopTimes",
                newName: "Fk_stopId");

            migrationBuilder.RenameIndex(
                name: "IX_StopTimes_FkTripId",
                table: "StopTimes",
                newName: "IX_StopTimes_Fk_tripId");

            migrationBuilder.RenameIndex(
                name: "IX_StopTimes_FkStopId",
                table: "StopTimes",
                newName: "IX_StopTimes_Fk_stopId");

            migrationBuilder.RenameColumn(
                name: "FkAgencyId",
                table: "Stops",
                newName: "Fk_agencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Stops_FkAgencyId",
                table: "Stops",
                newName: "IX_Stops_Fk_agencyId");

            migrationBuilder.RenameColumn(
                name: "FkAgencyId",
                table: "Routes",
                newName: "Fk_agencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_FkAgencyId",
                table: "Routes",
                newName: "IX_Routes_Fk_agencyId");

            migrationBuilder.RenameColumn(
                name: "FkAgencyId",
                table: "FeedInfoTbl",
                newName: "Fk_agencyId");

            migrationBuilder.RenameIndex(
                name: "IX_FeedInfoTbl_FkAgencyId",
                table: "FeedInfoTbl",
                newName: "IX_FeedInfoTbl_Fk_agencyId");

            migrationBuilder.RenameColumn(
                name: "FkFareId",
                table: "FareAttributesTbl",
                newName: "Fk_fareId");

            migrationBuilder.RenameIndex(
                name: "IX_FareAttributesTbl_FkFareId",
                table: "FareAttributesTbl",
                newName: "IX_FareAttributesTbl_Fk_fareId");

            migrationBuilder.RenameColumn(
                name: "FkParentAgencyId",
                table: "Agencies",
                newName: "Fk_parentAgencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Agencies_FkParentAgencyId",
                table: "Agencies",
                newName: "IX_Agencies_Fk_parentAgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agencies_ParentAgencies_Fk_parentAgencyId",
                table: "Agencies",
                column: "Fk_parentAgencyId",
                principalTable: "ParentAgencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FareAttributesTbl_Fares_Fk_fareId",
                table: "FareAttributesTbl",
                column: "Fk_fareId",
                principalTable: "Fares",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedInfoTbl_Agencies_Fk_agencyId",
                table: "FeedInfoTbl",
                column: "Fk_agencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Agencies_Fk_agencyId",
                table: "Routes",
                column: "Fk_agencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Agencies_Fk_agencyId",
                table: "Stops",
                column: "Fk_agencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StopTimes_Stops_Fk_stopId",
                table: "StopTimes",
                column: "Fk_stopId",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StopTimes_Trips_Fk_tripId",
                table: "StopTimes",
                column: "Fk_tripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Stops_Fk_fromStopId",
                table: "Transfers",
                column: "Fk_fromStopId",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Stops_Fk_toStopId",
                table: "Transfers",
                column: "Fk_toStopId",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Routes_Fk_routeId",
                table: "Trips",
                column: "Fk_routeId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agencies_ParentAgencies_Fk_parentAgencyId",
                table: "Agencies");

            migrationBuilder.DropForeignKey(
                name: "FK_FareAttributesTbl_Fares_Fk_fareId",
                table: "FareAttributesTbl");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedInfoTbl_Agencies_Fk_agencyId",
                table: "FeedInfoTbl");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Agencies_Fk_agencyId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Agencies_Fk_agencyId",
                table: "Stops");

            migrationBuilder.DropForeignKey(
                name: "FK_StopTimes_Stops_Fk_stopId",
                table: "StopTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_StopTimes_Trips_Fk_tripId",
                table: "StopTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Stops_Fk_fromStopId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Stops_Fk_toStopId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Routes_Fk_routeId",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "Fk_routeId",
                table: "Trips",
                newName: "FkRouteId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_Fk_routeId",
                table: "Trips",
                newName: "IX_Trips_FkRouteId");

            migrationBuilder.RenameColumn(
                name: "Fk_toStopId",
                table: "Transfers",
                newName: "ToStopId1");

            migrationBuilder.RenameColumn(
                name: "Fk_fromStopId",
                table: "Transfers",
                newName: "FromStopId1");

            migrationBuilder.RenameIndex(
                name: "IX_Transfers_Fk_toStopId",
                table: "Transfers",
                newName: "IX_Transfers_ToStopId1");

            migrationBuilder.RenameIndex(
                name: "IX_Transfers_Fk_fromStopId",
                table: "Transfers",
                newName: "IX_Transfers_FromStopId1");

            migrationBuilder.RenameColumn(
                name: "TripId",
                table: "StopTimes",
                newName: "GtfsTripId");

            migrationBuilder.RenameColumn(
                name: "StopId",
                table: "StopTimes",
                newName: "GtfsStopId");

            migrationBuilder.RenameColumn(
                name: "Fk_tripId",
                table: "StopTimes",
                newName: "FkTripId");

            migrationBuilder.RenameColumn(
                name: "Fk_stopId",
                table: "StopTimes",
                newName: "FkStopId");

            migrationBuilder.RenameIndex(
                name: "IX_StopTimes_Fk_tripId",
                table: "StopTimes",
                newName: "IX_StopTimes_FkTripId");

            migrationBuilder.RenameIndex(
                name: "IX_StopTimes_Fk_stopId",
                table: "StopTimes",
                newName: "IX_StopTimes_FkStopId");

            migrationBuilder.RenameColumn(
                name: "Fk_agencyId",
                table: "Stops",
                newName: "FkAgencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Stops_Fk_agencyId",
                table: "Stops",
                newName: "IX_Stops_FkAgencyId");

            migrationBuilder.RenameColumn(
                name: "Fk_agencyId",
                table: "Routes",
                newName: "FkAgencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_Fk_agencyId",
                table: "Routes",
                newName: "IX_Routes_FkAgencyId");

            migrationBuilder.RenameColumn(
                name: "Fk_agencyId",
                table: "FeedInfoTbl",
                newName: "FkAgencyId");

            migrationBuilder.RenameIndex(
                name: "IX_FeedInfoTbl_Fk_agencyId",
                table: "FeedInfoTbl",
                newName: "IX_FeedInfoTbl_FkAgencyId");

            migrationBuilder.RenameColumn(
                name: "Fk_fareId",
                table: "FareAttributesTbl",
                newName: "FkFareId");

            migrationBuilder.RenameIndex(
                name: "IX_FareAttributesTbl_Fk_fareId",
                table: "FareAttributesTbl",
                newName: "IX_FareAttributesTbl_FkFareId");

            migrationBuilder.RenameColumn(
                name: "Fk_parentAgencyId",
                table: "Agencies",
                newName: "FkParentAgencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Agencies_Fk_parentAgencyId",
                table: "Agencies",
                newName: "IX_Agencies_FkParentAgencyId");

            migrationBuilder.AddColumn<int>(
                name: "FkFromStopId",
                table: "Transfers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FkToStopId",
                table: "Transfers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Agencies_ParentAgencies_FkParentAgencyId",
                table: "Agencies",
                column: "FkParentAgencyId",
                principalTable: "ParentAgencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FareAttributesTbl_Fares_FkFareId",
                table: "FareAttributesTbl",
                column: "FkFareId",
                principalTable: "Fares",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedInfoTbl_Agencies_FkAgencyId",
                table: "FeedInfoTbl",
                column: "FkAgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Agencies_FkAgencyId",
                table: "Routes",
                column: "FkAgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Agencies_FkAgencyId",
                table: "Stops",
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
                name: "FK_Transfers_Stops_FromStopId1",
                table: "Transfers",
                column: "FromStopId1",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Stops_ToStopId1",
                table: "Transfers",
                column: "ToStopId1",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Routes_FkRouteId",
                table: "Trips",
                column: "FkRouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
