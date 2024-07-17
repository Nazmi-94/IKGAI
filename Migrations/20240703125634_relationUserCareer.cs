using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKGAi.Migrations
{
    /// <inheritdoc />
    public partial class relationUserCareer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userNumber",
                table: "Comment");

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Career",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Career_userId",
                table: "Career",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Career_User_userId",
                table: "Career",
                column: "userId",
                principalTable: "User",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Career_User_userId",
                table: "Career");

            migrationBuilder.DropIndex(
                name: "IX_Career_userId",
                table: "Career");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Career");

            migrationBuilder.AddColumn<int>(
                name: "userNumber",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
