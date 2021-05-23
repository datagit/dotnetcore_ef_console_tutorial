using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnetcore_ef_console_tutorial.Migrations
{
    public partial class V4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTags_Article_ArticleId",
                table: "ArticleTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTags_Tag_TagId",
                table: "ArticleTags");

            migrationBuilder.DropIndex(
                name: "IX_ArticleTags_ArticleId_TagId",
                table: "ArticleTags");

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "ArticleTags",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "ArticleTags",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTags_ArticleId_TagId",
                table: "ArticleTags",
                columns: new[] { "ArticleId", "TagId" },
                unique: true,
                filter: "[ArticleId] IS NOT NULL AND [TagId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTags_Article_ArticleId",
                table: "ArticleTags",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTags_Tag_TagId",
                table: "ArticleTags",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTags_Article_ArticleId",
                table: "ArticleTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTags_Tag_TagId",
                table: "ArticleTags");

            migrationBuilder.DropIndex(
                name: "IX_ArticleTags_ArticleId_TagId",
                table: "ArticleTags");

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "ArticleTags",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "ArticleTags",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTags_ArticleId_TagId",
                table: "ArticleTags",
                columns: new[] { "ArticleId", "TagId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTags_Article_ArticleId",
                table: "ArticleTags",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTags_Tag_TagId",
                table: "ArticleTags",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
