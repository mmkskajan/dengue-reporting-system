using Microsoft.EntityFrameworkCore.Migrations;

namespace CIDRS.Infrastructure.Migrations
{
    public partial class AddedPoliceRefToCO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PoliceId",
                schema: "pub",
                table: "ChiefOccupants",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChiefOccupants_PoliceId",
                schema: "pub",
                table: "ChiefOccupants",
                column: "PoliceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiefOccupants_Polices_PoliceId",
                schema: "pub",
                table: "ChiefOccupants",
                column: "PoliceId",
                principalSchema: "pub",
                principalTable: "Polices",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiefOccupants_Polices_PoliceId",
                schema: "pub",
                table: "ChiefOccupants");

            migrationBuilder.DropIndex(
                name: "IX_ChiefOccupants_PoliceId",
                schema: "pub",
                table: "ChiefOccupants");

            migrationBuilder.DropColumn(
                name: "PoliceId",
                schema: "pub",
                table: "ChiefOccupants");
        }
    }
}
