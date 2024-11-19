using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivityManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedUserIdForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Activities_CreatedUserId",
                table: "Activities",
                column: "CreatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_CreatedUserId",
                table: "Activities",
                column: "CreatedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_CreatedUserId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_CreatedUserId",
                table: "Activities");
        }
    }
}
