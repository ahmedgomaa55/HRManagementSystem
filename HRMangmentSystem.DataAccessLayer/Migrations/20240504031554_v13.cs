using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMangmentSystem.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class v13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnTime",
                table: "AttendanceRecords");

            migrationBuilder.AlterColumn<int>(
                name: "LateHours",
                table: "AttendanceRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "EarlyLeaveHours",
                table: "AttendanceRecords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OvertimeHours",
                table: "AttendanceRecords",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EarlyLeaveHours",
                table: "AttendanceRecords");

            migrationBuilder.DropColumn(
                name: "OvertimeHours",
                table: "AttendanceRecords");

            migrationBuilder.AlterColumn<int>(
                name: "LateHours",
                table: "AttendanceRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnTime",
                table: "AttendanceRecords",
                type: "bit",
                nullable: true);
        }
    }
}
