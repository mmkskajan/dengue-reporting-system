using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CIDRS.Infrastructure.Migrations
{
    public partial class PoliceTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Polices",
                schema: "pub",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    ArchivedAt = table.Column<DateTime>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    PoliceStationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Polices_PoliceStations_PoliceStationId",
                        column: x => x.PoliceStationId,
                        principalSchema: "mas",
                        principalTable: "PoliceStations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Polices_PoliceStationId",
                schema: "pub",
                table: "Polices",
                column: "PoliceStationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Polices",
                schema: "pub");
        }
    }
}
