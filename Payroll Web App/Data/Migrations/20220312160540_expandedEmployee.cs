using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll_Web_App.Data.Migrations
{
    public partial class expandedEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    SIN = table.Column<string>(nullable: true),
                    department = table.Column<string>(nullable: true),
                    hourlyRate = table.Column<int>(nullable: true),
                    annualRate = table.Column<int>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
