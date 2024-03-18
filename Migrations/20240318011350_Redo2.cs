using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GtfsApi.Migrations
{
    /// <inheritdoc />
    public partial class Redo2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FareAttributesEnumerable_Fares_FkFareId",
                table: "FareAttributesEnumerable");

            migrationBuilder.DropForeignKey(
                name: "FK_GtfsRoutes_Agencies_FkAgencyId",
                table: "GtfsRoutes");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_GtfsRoutes_FkRouteId",
                table: "Trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GtfsRoutes",
                table: "GtfsRoutes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FareAttributesEnumerable",
                table: "FareAttributesEnumerable");

            migrationBuilder.RenameTable(
                name: "GtfsRoutes",
                newName: "Routes");

            migrationBuilder.RenameTable(
                name: "FareAttributesEnumerable",
                newName: "FareAttributesTbl");

            migrationBuilder.RenameIndex(
                name: "IX_GtfsRoutes_FkAgencyId",
                table: "Routes",
                newName: "IX_Routes_FkAgencyId");

            migrationBuilder.RenameIndex(
                name: "IX_FareAttributesEnumerable_FkFareId",
                table: "FareAttributesTbl",
                newName: "IX_FareAttributesTbl_FkFareId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Routes",
                table: "Routes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FareAttributesTbl",
                table: "FareAttributesTbl",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FareAttributesTbl_Fares_FkFareId",
                table: "FareAttributesTbl",
                column: "FkFareId",
                principalTable: "Fares",
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
                name: "FK_Trips_Routes_FkRouteId",
                table: "Trips",
                column: "FkRouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FareAttributesTbl_Fares_FkFareId",
                table: "FareAttributesTbl");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Agencies_FkAgencyId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Routes_FkRouteId",
                table: "Trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Routes",
                table: "Routes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FareAttributesTbl",
                table: "FareAttributesTbl");

            migrationBuilder.RenameTable(
                name: "Routes",
                newName: "GtfsRoutes");

            migrationBuilder.RenameTable(
                name: "FareAttributesTbl",
                newName: "FareAttributesEnumerable");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_FkAgencyId",
                table: "GtfsRoutes",
                newName: "IX_GtfsRoutes_FkAgencyId");

            migrationBuilder.RenameIndex(
                name: "IX_FareAttributesTbl_FkFareId",
                table: "FareAttributesEnumerable",
                newName: "IX_FareAttributesEnumerable_FkFareId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GtfsRoutes",
                table: "GtfsRoutes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FareAttributesEnumerable",
                table: "FareAttributesEnumerable",
                column: "Id");

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
                name: "FK_Trips_GtfsRoutes_FkRouteId",
                table: "Trips",
                column: "FkRouteId",
                principalTable: "GtfsRoutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
