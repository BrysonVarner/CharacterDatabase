using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterDatabase.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditedUserInfoToCharacterProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserInfo",
                table: "Characters");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Characters",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_UserId",
                table: "Characters",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_AspNetUsers_UserId",
                table: "Characters",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_AspNetUsers_UserId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_UserId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Characters");

            migrationBuilder.AddColumn<string>(
                name: "UserInfo",
                table: "Characters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
