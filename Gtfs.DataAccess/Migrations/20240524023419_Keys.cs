using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gtfs.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Keys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendars_Agencies_AgencyId",
                table: "Calendars");

            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Agencies_AgencyId",
                table: "Stops");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Shapes_ShapeId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_ShapeId",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "AgencyId",
                table: "Stops",
                newName: "SourceId");

            migrationBuilder.RenameIndex(
                name: "IX_Stops_AgencyId",
                table: "Stops",
                newName: "IX_Stops_SourceId");

            migrationBuilder.RenameColumn(
                name: "AgencyId",
                table: "Calendars",
                newName: "SourceId");

            migrationBuilder.RenameIndex(
                name: "IX_Calendars_AgencyId",
                table: "Calendars",
                newName: "IX_Calendars_SourceId");

            migrationBuilder.AddColumn<int>(
                name: "SourceId",
                table: "Trips",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_SourceId",
                table: "Trips",
                column: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_Sources_SourceId",
                table: "Calendars",
                column: "SourceId",
                principalTable: "Sources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Sources_SourceId",
                table: "Stops",
                column: "SourceId",
                principalTable: "Sources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Sources_SourceId",
                table: "Trips",
                column: "SourceId",
                principalTable: "Sources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendars_Sources_SourceId",
                table: "Calendars");

            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Sources_SourceId",
                table: "Stops");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Sources_SourceId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_SourceId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "Stops",
                newName: "AgencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Stops_SourceId",
                table: "Stops",
                newName: "IX_Stops_AgencyId");

            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "Calendars",
                newName: "AgencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Calendars_SourceId",
                table: "Calendars",
                newName: "IX_Calendars_AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_ShapeId",
                table: "Trips",
                column: "ShapeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_Agencies_AgencyId",
                table: "Calendars",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Agencies_AgencyId",
                table: "Stops",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Shapes_ShapeId",
                table: "Trips",
                column: "ShapeId",
                principalTable: "Shapes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
