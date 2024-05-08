using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webtintuc.Migrations
{
    /// <inheritdoc />
    public partial class editNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_news_author_AuID",
                table: "news");

            migrationBuilder.DropForeignKey(
                name: "FK_news_category_CateID",
                table: "news");

            migrationBuilder.RenameColumn(
                name: "CateID",
                table: "news",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "AuID",
                table: "news",
                newName: "AuthorID");

            migrationBuilder.RenameIndex(
                name: "IX_news_CateID",
                table: "news",
                newName: "IX_news_CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_news_AuID",
                table: "news",
                newName: "IX_news_AuthorID");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_news_author_AuthorID",
                table: "news");

            migrationBuilder.DropForeignKey(
                name: "FK_news_category_CategoryID",
                table: "news");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "news",
                newName: "CateID");

            migrationBuilder.RenameColumn(
                name: "AuthorID",
                table: "news",
                newName: "AuID");

            migrationBuilder.RenameIndex(
                name: "IX_news_CategoryID",
                table: "news",
                newName: "IX_news_CateID");

            migrationBuilder.RenameIndex(
                name: "IX_news_AuthorID",
                table: "news",
                newName: "IX_news_AuID");

            migrationBuilder.AddForeignKey(
                name: "FK_news_author_AuID",
                table: "news",
                column: "AuID",
                principalTable: "author",
                principalColumn: "AuthorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_news_category_CateID",
                table: "news",
                column: "CateID",
                principalTable: "category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
