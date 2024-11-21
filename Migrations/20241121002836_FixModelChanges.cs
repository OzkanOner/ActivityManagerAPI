using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivityManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActivities_Activities_ActivityId1",
                table: "UserActivities");

            migrationBuilder.DropIndex(
                name: "IX_UserActivities_ActivityId",
                table: "UserActivities");

            migrationBuilder.DropIndex(
                name: "IX_UserActivities_ActivityId1",
                table: "UserActivities");

            migrationBuilder.DropColumn(
                name: "ActivityId1",
                table: "UserActivities");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_ActivityId",
                table: "UserActivities",
                column: "ActivityId");
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

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_ActivityId",
                table: "UserActivities",
                column: "ActivityId",
                unique: true);

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
    }
}
