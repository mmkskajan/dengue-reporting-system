using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CIDRS.Infrastructure.Migrations
{
    public partial class PhiTableInitiated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PublicHealthInspectors",
                schema: "moh",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    ArchivedAt = table.Column<DateTime>(nullable: true),
                    IdentityUserId = table.Column<string>(nullable: true),
                    DistrictId = table.Column<int>(nullable: false),
                    MohAreaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicHealthInspectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicHealthInspectors_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalSchema: "mas",
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicHealthInspectors_MohAreas_MohAreaId",
                        column: x => x.MohAreaId,
                        principalSchema: "mas",
                        principalTable: "MohAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
               name: "IX_PublicHealthInspectors_DistrictId",
               schema: "moh",
               table: "PublicHealthInspectors",
               column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicHealthInspectors_MohAreaId",
                schema: "moh",
                table: "PublicHealthInspectors",
                column: "MohAreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicHealthInspectors",
                schema: "moh");

        }
    }
}
