using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReservationStatusUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusName",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Reservations",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Reservations",
                newName: "StatusId");

            migrationBuilder.AddColumn<string>(
                name: "StatusName",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
