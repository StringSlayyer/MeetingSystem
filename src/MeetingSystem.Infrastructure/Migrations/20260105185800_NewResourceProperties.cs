using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewResourceProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Resources",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Resources",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Features",
                table: "Resources",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Resources",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerHour",
                table: "Resources",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Features",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "PricePerHour",
                table: "Resources");
        }
    }
}
