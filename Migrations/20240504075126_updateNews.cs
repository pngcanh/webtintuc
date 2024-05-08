using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webtintuc.Migrations
{
    /// <inheritdoc />
    public partial class updateNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_news_author_AuthorID",
                table: "news");

            migrationBuilder.DropForeignKey(
                name: "FK_news_category_CategoryID",
                table: "news");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "news",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorID",
                table: "news",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_news_author_AuthorID",
                table: "news",
                column: "AuthorID",
                principalTable: "author",
                principalColumn: "AuthorID");

            migrationBuilder.AddForeignKey(
                name: "FK_news_category_CategoryID",
                table: "news",
                column: "CategoryID",
                principalTable: "category",
                principalColumn: "CategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_news_author_AuthorID",
                table: "news");

            migrationBuilder.DropForeignKey(
                name: "FK_news_category_CategoryID",
                table: "news");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "news",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorID",
                table: "news",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_news_author_AuthorID",
                table: "news",
                column: "AuthorID",
                principalTable: "author",
                principalColumn: "AuthorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_news_category_CategoryID",
                table: "news",
                column: "CategoryID",
                principalTable: "category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
