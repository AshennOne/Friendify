using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class OriginalAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OriginalAuthorId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_OriginalAuthorId",
                table: "Posts",
                column: "OriginalAuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_OriginalAuthorId",
                table: "Posts",
                column: "OriginalAuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_OriginalAuthorId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_OriginalAuthorId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "OriginalAuthorId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "AspNetUsers");
        }
    }
}
