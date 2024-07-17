using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKGAi.Migrations
{
    /// <inheritdoc />
    public partial class testingChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userId",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "commentId",
                table: "Comment",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "careerId",
                table: "Career",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Comment",
                newName: "commentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Career",
                newName: "careerId");
        }
    }
}
