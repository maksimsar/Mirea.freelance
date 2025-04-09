using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class firstigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Profiles");

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "StudentProfiles",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "StudentProfiles");

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "Profiles",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
