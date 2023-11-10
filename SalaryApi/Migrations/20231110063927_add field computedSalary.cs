using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryApi.Migrations
{
    /// <inheritdoc />
    public partial class addfieldcomputedSalary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Salary",
                keyColumn: "Id",
                keyValue: new Guid("3706b853-57f2-4f54-be81-56011fa4393d"));

            migrationBuilder.AddColumn<long>(
                name: "ComputedSalary",
                table: "Salary",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.InsertData(
                table: "Salary",
                columns: new[] { "Id", "Allowance", "BasicSalary", "ComputedSalary", "Date", "EmployeeId", "FirstName", "LastName", "Transportation" },
                values: new object[] { new Guid("bf0acd96-3506-4acc-bc43-0a7d33eaf712"), 300000L, 10000000L, 0L, "14020801", 100L, "Ali", "Ahmadi", 500000L });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Salary",
                keyColumn: "Id",
                keyValue: new Guid("bf0acd96-3506-4acc-bc43-0a7d33eaf712"));

            migrationBuilder.DropColumn(
                name: "ComputedSalary",
                table: "Salary");

            migrationBuilder.InsertData(
                table: "Salary",
                columns: new[] { "Id", "Allowance", "BasicSalary", "Date", "EmployeeId", "FirstName", "LastName", "Transportation" },
                values: new object[] { new Guid("3706b853-57f2-4f54-be81-56011fa4393d"), 300000L, 10000000L, "14020801", 100L, "Ali", "Ahmadi", 500000L });
        }
    }
}
