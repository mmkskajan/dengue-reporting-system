using Microsoft.EntityFrameworkCore.Migrations;

namespace CIDRS.Identity.Infrastructure.Migrations
{
    public partial class AddColumnToUerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "hasTempPassword",
                schema: "identity",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hasTempPassword",
                schema: "identity",
                table: "AspNetUsers");
        }
    }
}
