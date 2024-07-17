using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKGAi.Migrations
{
    /// <inheritdoc />
    public partial class relationUserComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userNumber",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_userId",
                table: "Comment",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_userId",
                table: "Comment",
                column: "userId",
                principalTable: "User",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_userId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_userId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "userNumber",
                table: "Comment");
        }
    }
}
