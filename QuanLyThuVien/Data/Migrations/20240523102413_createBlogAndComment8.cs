using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThuVien.Data.Migrations
{
    /// <inheritdoc />
    public partial class createBlogAndComment8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_AspNetUsers_UserId",
                table: "Blog");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Blog",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_UserId",
                table: "Blog",
                newName: "IX_Blog_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_AspNetUsers_ApplicationUserId",
                table: "Blog",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_AspNetUsers_ApplicationUserId",
                table: "Blog");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Blog",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_ApplicationUserId",
                table: "Blog",
                newName: "IX_Blog_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_AspNetUsers_UserId",
                table: "Blog",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
