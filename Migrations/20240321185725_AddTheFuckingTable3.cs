using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GtfsApi.Migrations
{
    /// <inheritdoc />
    public partial class AddTheFuckingTable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeedInfoTbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FeedPublisherName = table.Column<string>(type: "TEXT", nullable: false),
                    FeedPublisherUrl = table.Column<string>(type: "TEXT", nullable: false),
                    FeedLanguage = table.Column<string>(type: "TEXT", nullable: true),
                    FeedStartDate = table.Column<string>(type: "TEXT", nullable: false),
                    FeedEndDate = table.Column<string>(type: "TEXT", nullable: false),
                    FeedVersion = table.Column<string>(type: "TEXT", nullable: false),
                    FkAgencyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedInfoTbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedInfoTbl_Agencies_FkAgencyId",
                        column: x => x.FkAgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedInfoTbl_FkAgencyId",
                table: "FeedInfoTbl",
                column: "FkAgencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedInfoTbl");
        }
    }
}
