using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CIDRS.Infrastructure.Migrations
{
    public partial class Init_AppDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mas");

            migrationBuilder.CreateTable(
                name: "Districts",
                schema: "mas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    ArchivedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MohAreas",
                schema: "mas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    ArchivedAt = table.Column<DateTime>(nullable: true),
                    DistrictId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MohAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MohAreas_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalSchema: "mas",
                        principalTable: "Districts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PoliceStations",
                schema: "mas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    ArchivedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    MohAreaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceStations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliceStations_MohAreas_MohAreaId",
                        column: x => x.MohAreaId,
                        principalSchema: "mas",
                        principalTable: "MohAreas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MohAreas_DistrictId",
                schema: "mas",
                table: "MohAreas",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliceStations_MohAreaId",
                schema: "mas",
                table: "PoliceStations",
                column: "MohAreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoliceStations",
                schema: "mas");

            migrationBuilder.DropTable(
                name: "MohAreas",
                schema: "mas");

            migrationBuilder.DropTable(
                name: "Districts",
                schema: "mas");
        }
    }
}
