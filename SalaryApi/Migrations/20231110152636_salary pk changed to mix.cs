using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryApi.Migrations
{
    /// <inheritdoc />
    public partial class salarypkchangedtomix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Salary",
                table: "Salary");

            migrationBuilder.DeleteData(
                table: "Salary",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("bf0acd96-3506-4acc-bc43-0a7d33eaf712"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Salary");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Salary",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Salary",
                table: "Salary",
                columns: new[] { "EmployeeId", "Date" });

            migrationBuilder.InsertData(
                table: "Salary",
                columns: new[] { "Date", "EmployeeId", "Allowance", "BasicSalary", "ComputedSalary", "FirstName", "LastName", "Transportation" },
                values: new object[] { "14020801", 100L, 300000L, 10000000L, 0L, "Ali", "Ahmadi", 500000L });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Salary",
                table: "Salary");

            migrationBuilder.DeleteData(
                table: "Salary",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { "14020801", 100L });

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Salary",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Salary",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Salary",
                table: "Salary",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Salary",
                columns: new[] { "Id", "Allowance", "BasicSalary", "ComputedSalary", "Date", "EmployeeId", "FirstName", "LastName", "Transportation" },
                values: new object[] { new Guid("bf0acd96-3506-4acc-bc43-0a7d33eaf712"), 300000L, 10000000L, 0L, "14020801", 100L, "Ali", "Ahmadi", 500000L });
        }
    }
}
