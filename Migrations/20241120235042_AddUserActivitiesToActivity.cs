using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivityManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserActivitiesToActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivityId1",
                table: "UserActivities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_ActivityId1",
                table: "UserActivities",
                column: "ActivityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivities_Activities_ActivityId1",
                table: "UserActivities",
                column: "ActivityId1",
                principalTable: "Activities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActivities_Activities_ActivityId1",
                table: "UserActivities");

            migrationBuilder.DropIndex(
                name: "IX_UserActivities_ActivityId1",
                table: "UserActivities");

            migrationBuilder.DropColumn(
                name: "ActivityId1",
                table: "UserActivities");
        }
    }
}
