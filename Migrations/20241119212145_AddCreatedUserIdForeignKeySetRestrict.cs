using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivityManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedUserIdForeignKeySetRestrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_CreatedUserId",
                table: "Activities");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_CreatedUserId",
                table: "Activities",
                newName: "IX_Activity_CreatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_CreatedUserId",
                table: "Activities",
                column: "CreatedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_CreatedUserId",
                table: "Activities");

            migrationBuilder.RenameIndex(
                name: "IX_Activity_CreatedUserId",
                table: "Activities",
                newName: "IX_Activities_CreatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_CreatedUserId",
                table: "Activities",
                column: "CreatedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
