using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMangmentSystem.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceRecords_Employees_EmployeeSSN",
                table: "AttendanceRecords");

            migrationBuilder.RenameColumn(
                name: "WeeklyHolidays",
                table: "GeneralSettings",
                newName: "WeeklyHoliday1");

            migrationBuilder.RenameColumn(
                name: "EmployeeSSN",
                table: "AttendanceRecords",
                newName: "EmployeeNationalId");

            migrationBuilder.RenameIndex(
                name: "IX_AttendanceRecords_EmployeeSSN",
                table: "AttendanceRecords",
                newName: "IX_AttendanceRecords_EmployeeNationalId");

            migrationBuilder.AddColumn<string>(
                name: "WeeklyHoliday2",
                table: "GeneralSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceRecords_Employees_EmployeeNationalId",
                table: "AttendanceRecords",
                column: "EmployeeNationalId",
                principalTable: "Employees",
                principalColumn: "NationalId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceRecords_Employees_EmployeeNationalId",
                table: "AttendanceRecords");

            migrationBuilder.DropColumn(
                name: "WeeklyHoliday2",
                table: "GeneralSettings");

            migrationBuilder.RenameColumn(
                name: "WeeklyHoliday1",
                table: "GeneralSettings",
                newName: "WeeklyHolidays");

            migrationBuilder.RenameColumn(
                name: "EmployeeNationalId",
                table: "AttendanceRecords",
                newName: "EmployeeSSN");

            migrationBuilder.RenameIndex(
                name: "IX_AttendanceRecords_EmployeeNationalId",
                table: "AttendanceRecords",
                newName: "IX_AttendanceRecords_EmployeeSSN");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceRecords_Employees_EmployeeSSN",
                table: "AttendanceRecords",
                column: "EmployeeSSN",
                principalTable: "Employees",
                principalColumn: "NationalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
