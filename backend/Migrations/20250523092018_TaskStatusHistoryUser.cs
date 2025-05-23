using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class TaskStatusHistoryUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChangedByUserId",
                table: "ProjectTaskStatusHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTaskStatusHistories_ChangedByUserId",
                table: "ProjectTaskStatusHistories",
                column: "ChangedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTaskStatusHistories_Users_ChangedByUserId",
                table: "ProjectTaskStatusHistories",
                column: "ChangedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTaskStatusHistories_Users_ChangedByUserId",
                table: "ProjectTaskStatusHistories");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTaskStatusHistories_ChangedByUserId",
                table: "ProjectTaskStatusHistories");

            migrationBuilder.DropColumn(
                name: "ChangedByUserId",
                table: "ProjectTaskStatusHistories");
        }
    }
}
