using Microsoft.EntityFrameworkCore.Migrations;

namespace CIDRS.Infrastructure.Migrations
{
    public partial class ModifiedRespectivePhi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PhiId",
                schema: "pub",
                table: "ChiefOccupants",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PhiId",
                schema: "pub",
                table: "ChiefOccupants",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
