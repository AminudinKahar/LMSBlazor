using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSBlazor.Plugins.SqlServer.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EmployeeAge = table.Column<int>(type: "int", nullable: false),
                    EmployeePhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalLeaveDays = table.Column<double>(type: "float", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Leaves",
                columns: table => new
                {
                    LeaveId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaves", x => x.LeaveId);
                });

            migrationBuilder.CreateTable(
                name: "Employee_Leaves",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LeaveId = table.Column<int>(type: "int", nullable: false),
                    DaysLeft = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_Leaves", x => new { x.LeaveId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_Employee_Leaves_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_Leaves_Leaves_LeaveId",
                        column: x => x.LeaveId,
                        principalTable: "Leaves",
                        principalColumn: "LeaveId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveApplicationLogs",
                columns: table => new
                {
                    LeaveApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LvNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    TotalDaysBefore = table.Column<double>(type: "float", nullable: false),
                    LeaveId = table.Column<int>(type: "int", nullable: false),
                    TotalDaysAfter = table.Column<double>(type: "float", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoneBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovalType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveApplicationLogs", x => x.LeaveApplicationId);
                    table.ForeignKey(
                        name: "FK_LeaveApplicationLogs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveApplicationLogs_Leaves_LeaveId",
                        column: x => x.LeaveId,
                        principalTable: "Leaves",
                        principalColumn: "LeaveId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveApplications",
                columns: table => new
                {
                    LeaveApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LvNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    TotalDays = table.Column<double>(type: "float", nullable: false),
                    LeaveId = table.Column<int>(type: "int", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoneBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovalType = table.Column<int>(type: "int", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFromApplyType = table.Column<int>(type: "int", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateToApplyType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveApplications", x => x.LeaveApplicationId);
                    table.ForeignKey(
                        name: "FK_LeaveApplications_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveApplications_Leaves_LeaveId",
                        column: x => x.LeaveId,
                        principalTable: "Leaves",
                        principalColumn: "LeaveId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "EmployeeAge", "EmployeeEmail", "EmployeeName", "EmployeePhoneNumber", "HireDate", "TotalLeaveDays" },
                values: new object[,]
                {
                    { 1, 30, "amin@idwal.com.my", "Aminudin Ab Kahar", "011-33272978", new DateTime(2022, 2, 18, 16, 3, 44, 437, DateTimeKind.Local).AddTicks(631), 54.0 },
                    { 2, 50, "mdNor@idwal.com.my", "Mohd Nor Bin Md Tan", "", new DateTime(2003, 3, 2, 16, 3, 44, 437, DateTimeKind.Local).AddTicks(660), 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Leaves",
                columns: new[] { "LeaveId", "LeaveName" },
                values: new object[,]
                {
                    { 1, "Cuti Tahunan" },
                    { 2, "Cuti Sakit" },
                    { 3, "Cuti Bersalin (Perempuan)" },
                    { 4, "Cuti Bersalin (Lelaki)" }
                });

            migrationBuilder.InsertData(
                table: "Employee_Leaves",
                columns: new[] { "EmployeeId", "LeaveId", "DaysLeft" },
                values: new object[] { 1, 1, 12.0 });

            migrationBuilder.InsertData(
                table: "Employee_Leaves",
                columns: new[] { "EmployeeId", "LeaveId", "DaysLeft" },
                values: new object[] { 1, 2, 12.0 });

            migrationBuilder.InsertData(
                table: "Employee_Leaves",
                columns: new[] { "EmployeeId", "LeaveId", "DaysLeft" },
                values: new object[] { 1, 4, 30.0 });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Leaves_EmployeeId",
                table: "Employee_Leaves",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplicationLogs_EmployeeId",
                table: "LeaveApplicationLogs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplicationLogs_LeaveId",
                table: "LeaveApplicationLogs",
                column: "LeaveId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplications_EmployeeId",
                table: "LeaveApplications",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplications_LeaveId",
                table: "LeaveApplications",
                column: "LeaveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee_Leaves");

            migrationBuilder.DropTable(
                name: "LeaveApplicationLogs");

            migrationBuilder.DropTable(
                name: "LeaveApplications");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Leaves");
        }
    }
}
