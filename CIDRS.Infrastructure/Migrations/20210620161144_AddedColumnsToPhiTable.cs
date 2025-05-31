using Microsoft.EntityFrameworkCore.Migrations;

namespace CIDRS.Infrastructure.Migrations
{
    public partial class AddedColumnsToPhiTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "moh",
                table: "PublicHealthInspectors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "moh",
                table: "PublicHealthInspectors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "moh",
                table: "PublicHealthInspectors");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "moh",
                table: "PublicHealthInspectors");
        }
    }
}
