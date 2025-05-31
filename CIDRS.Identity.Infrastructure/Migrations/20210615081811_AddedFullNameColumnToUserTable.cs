using Microsoft.EntityFrameworkCore.Migrations;

namespace CIDRS.Identity.Infrastructure.Migrations
{
    public partial class AddedFullNameColumnToUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "identity",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "identity",
                table: "AspNetUsers");
        }
    }
}
