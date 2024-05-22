using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webtintuc.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "newsPhoto");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "news",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "news");

            migrationBuilder.CreateTable(
                name: "newsPhoto",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsID = table.Column<int>(type: "int", nullable: true),
                    fileName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_newsPhoto", x => x.ID);
                    table.ForeignKey(
                        name: "FK_newsPhoto_news_NewsID",
                        column: x => x.NewsID,
                        principalTable: "news",
                        principalColumn: "NewsID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_newsPhoto_NewsID",
                table: "newsPhoto",
                column: "NewsID",
                unique: true,
                filter: "[NewsID] IS NOT NULL");
        }
    }
}
