using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryApi.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Salary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    BasicSalary = table.Column<long>(type: "bigint", nullable: false),
                    Allowance = table.Column<long>(type: "bigint", nullable: false),
                    Transportation = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salary", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Salary",
                columns: new[] { "Id", "Allowance", "BasicSalary", "Date", "EmployeeId", "FirstName", "LastName", "Transportation" },
                values: new object[] { new Guid("3706b853-57f2-4f54-be81-56011fa4393d"), 300000L, 10000000L, "14020801", 100L, "Ali", "Ahmadi", 500000L });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Salary");
        }
    }
}
