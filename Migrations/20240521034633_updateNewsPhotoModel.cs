using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webtintuc.Migrations
{
    /// <inheritdoc />
    public partial class updateNewsPhotoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_newsPhoto_news_NewsID",
                table: "newsPhoto");

            migrationBuilder.DropIndex(
                name: "IX_newsPhoto_NewsID",
                table: "newsPhoto");

            migrationBuilder.AlterColumn<int>(
                name: "NewsID",
                table: "newsPhoto",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_newsPhoto_NewsID",
                table: "newsPhoto",
                column: "NewsID",
                unique: true,
                filter: "[NewsID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_newsPhoto_news_NewsID",
                table: "newsPhoto",
                column: "NewsID",
                principalTable: "news",
                principalColumn: "NewsID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_newsPhoto_news_NewsID",
                table: "newsPhoto");

            migrationBuilder.DropIndex(
                name: "IX_newsPhoto_NewsID",
                table: "newsPhoto");

            migrationBuilder.AlterColumn<int>(
                name: "NewsID",
                table: "newsPhoto",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_newsPhoto_NewsID",
                table: "newsPhoto",
                column: "NewsID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_newsPhoto_news_NewsID",
                table: "newsPhoto",
                column: "NewsID",
                principalTable: "news",
                principalColumn: "NewsID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
