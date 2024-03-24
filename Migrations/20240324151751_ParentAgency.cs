using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GtfsApi.Migrations
{
    /// <inheritdoc />
    public partial class ParentAgency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Stops_FromStopId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Stops_ToStopId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_FromStopId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_ToStopId",
                table: "Transfers");

            migrationBuilder.AlterColumn<string>(
                name: "ToStopId",
                table: "Transfers",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "FromStopId",
                table: "Transfers",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "FromStopId1",
                table: "Transfers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToStopId1",
                table: "Transfers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "GtfsStopId",
                table: "StopTimes",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "StopId",
                table: "Stops",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "FkParentAgencyId",
                table: "Agencies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ParentAgencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentAgencies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_FromStopId1",
                table: "Transfers",
                column: "FromStopId1");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ToStopId1",
                table: "Transfers",
                column: "ToStopId1");

            migrationBuilder.CreateIndex(
                name: "IX_Agencies_FkParentAgencyId",
                table: "Agencies",
                column: "FkParentAgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agencies_ParentAgencies_FkParentAgencyId",
                table: "Agencies",
                column: "FkParentAgencyId",
                principalTable: "ParentAgencies",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agencies_ParentAgencies_FkParentAgencyId",
                table: "Agencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Stops_FromStopId1",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Stops_ToStopId1",
                table: "Transfers");

            migrationBuilder.DropTable(
                name: "ParentAgencies");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_FromStopId1",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_ToStopId1",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Agencies_FkParentAgencyId",
                table: "Agencies");

            migrationBuilder.DropColumn(
                name: "FromStopId1",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "ToStopId1",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "FkParentAgencyId",
                table: "Agencies");

            migrationBuilder.AlterColumn<int>(
                name: "ToStopId",
                table: "Transfers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "FromStopId",
                table: "Transfers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "GtfsStopId",
                table: "StopTimes",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "StopId",
                table: "Stops",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_FromStopId",
                table: "Transfers",
                column: "FromStopId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ToStopId",
                table: "Transfers",
                column: "ToStopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Stops_FromStopId",
                table: "Transfers",
                column: "FromStopId",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Stops_ToStopId",
                table: "Transfers",
                column: "ToStopId",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
