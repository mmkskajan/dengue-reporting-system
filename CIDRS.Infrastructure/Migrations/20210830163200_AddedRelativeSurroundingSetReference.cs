using Microsoft.EntityFrameworkCore.Migrations;

namespace CIDRS.Infrastructure.Migrations
{
    public partial class AddedRelativeSurroundingSetReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RelativeId",
                schema: "app",
                table: "SurroundingSets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurroundingSets_RelativeId",
                schema: "app",
                table: "SurroundingSets",
                column: "RelativeId",
                unique: true,
                filter: "[RelativeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SurroundingSets_SurroundingSets_RelativeId",
                schema: "app",
                table: "SurroundingSets",
                column: "RelativeId",
                principalSchema: "app",
                principalTable: "SurroundingSets",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurroundingSets_SurroundingSets_RelativeId",
                schema: "app",
                table: "SurroundingSets");

            migrationBuilder.DropIndex(
                name: "IX_SurroundingSets_RelativeId",
                schema: "app",
                table: "SurroundingSets");

            migrationBuilder.DropColumn(
                name: "RelativeId",
                schema: "app",
                table: "SurroundingSets");
        }
    }
}
