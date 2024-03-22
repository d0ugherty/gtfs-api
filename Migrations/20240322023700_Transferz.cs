using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GtfsApi.Migrations
{
    /// <inheritdoc />
    public partial class Transferz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FromStopId = table.Column<int>(type: "INTEGER", nullable: false),
                    ToStopId = table.Column<int>(type: "INTEGER", nullable: false),
                    TransferType = table.Column<int>(type: "INTEGER", nullable: true),
                    MinTransferTime = table.Column<int>(type: "INTEGER", nullable: true),
                    FkFromStopId = table.Column<int>(type: "INTEGER", nullable: false),
                    FkToStopId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_Stops_FromStopId",
                        column: x => x.FromStopId,
                        principalTable: "Stops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transfers_Stops_ToStopId",
                        column: x => x.ToStopId,
                        principalTable: "Stops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_FromStopId",
                table: "Transfers",
                column: "FromStopId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ToStopId",
                table: "Transfers",
                column: "ToStopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transfers");
        }
    }
}
