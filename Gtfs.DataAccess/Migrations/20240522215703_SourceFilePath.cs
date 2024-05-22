using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gtfs.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SourceFilePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Sources",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Sources");
        }
    }
}
