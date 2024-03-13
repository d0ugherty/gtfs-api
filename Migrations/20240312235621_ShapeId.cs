using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GtfsApi.Migrations
{
    /// <inheritdoc />
    public partial class ShapeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Shapes_ShapeId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_ShapeId",
                table: "Trips");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Trips_ShapeId",
                table: "Trips",
                column: "ShapeId");

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
