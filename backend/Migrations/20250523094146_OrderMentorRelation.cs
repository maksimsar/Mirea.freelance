using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class OrderMentorRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MentorProfileId",
                table: "Orders",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MentorProfileId",
                table: "Orders",
                column: "MentorProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_MentorProfiles_MentorProfileId",
                table: "Orders",
                column: "MentorProfileId",
                principalTable: "MentorProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_MentorProfiles_MentorProfileId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_MentorProfileId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MentorProfileId",
                table: "Orders");
        }
    }
}
