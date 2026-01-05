using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewCompanyProperties2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingCount",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Companies",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingCount",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Companies");
        }
    }
}
