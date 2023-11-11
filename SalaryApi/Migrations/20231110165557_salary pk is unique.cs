using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryApi.Migrations
{
    /// <inheritdoc />
    public partial class salarypkisunique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Salary_EmployeeId_Date",
                table: "Salary");

            migrationBuilder.CreateIndex(
                name: "IX_Salary_EmployeeId_Date",
                table: "Salary",
                columns: new[] { "EmployeeId", "Date" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Salary_EmployeeId_Date",
                table: "Salary");

            migrationBuilder.CreateIndex(
                name: "IX_Salary_EmployeeId_Date",
                table: "Salary",
                columns: new[] { "EmployeeId", "Date" });
        }
    }
}
