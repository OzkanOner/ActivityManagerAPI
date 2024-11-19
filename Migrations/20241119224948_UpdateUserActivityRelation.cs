using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivityManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserActivityRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActivities_Activities_ActivityId1",
                table: "UserActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_UserActivities_Users_UserId1",
                table: "UserActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_UserActivities_Users_UserId2",
                table: "UserActivities");

            migrationBuilder.DropIndex(
                name: "IX_UserActivities_ActivityId",
                table: "UserActivities");

            migrationBuilder.DropIndex(
                name: "IX_UserActivities_ActivityId1",
                table: "UserActivities");

            migrationBuilder.DropIndex(
                name: "IX_UserActivities_UserId1",
                table: "UserActivities");

            migrationBuilder.DropIndex(
                name: "IX_UserActivities_UserId2",
                table: "UserActivities");

            migrationBuilder.DropColumn(
                name: "ActivityId1",
                table: "UserActivities");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserActivities");

            migrationBuilder.DropColumn(
                name: "UserId2",
                table: "UserActivities");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_ActivityId",
                table: "UserActivities",
                column: "ActivityId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserActivities_ActivityId",
                table: "UserActivities");

            migrationBuilder.AddColumn<int>(
                name: "ActivityId1",
                table: "UserActivities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "UserActivities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId2",
                table: "UserActivities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_ActivityId",
                table: "UserActivities",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_ActivityId1",
                table: "UserActivities",
                column: "ActivityId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_UserId1",
                table: "UserActivities",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_UserId2",
                table: "UserActivities",
                column: "UserId2");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivities_Activities_ActivityId1",
                table: "UserActivities",
                column: "ActivityId1",
                principalTable: "Activities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivities_Users_UserId1",
                table: "UserActivities",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivities_Users_UserId2",
                table: "UserActivities",
                column: "UserId2",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
