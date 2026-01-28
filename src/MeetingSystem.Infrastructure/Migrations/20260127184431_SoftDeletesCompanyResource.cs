using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SoftDeletesCompanyResource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Resources",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Companies");
        }
    }
}
