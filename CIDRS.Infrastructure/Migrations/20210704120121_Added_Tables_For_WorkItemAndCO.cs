using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CIDRS.Infrastructure.Migrations
{
    public partial class Added_Tables_For_WorkItemAndCO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "app");

            migrationBuilder.EnsureSchema(
                name: "pub");

            migrationBuilder.EnsureSchema(
                name: "wkitm");

            migrationBuilder.CreateTable(
                name: "ChiefOccupants",
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
                    PhoneNumber = table.Column<string>(nullable: true),
                    IdentityUserId = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ApplicationRejectedCount = table.Column<int>(nullable: false),
                    DistrictId = table.Column<int>(nullable: false),
                    MohAreaId = table.Column<int>(nullable: false),
                    PhiId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiefOccupants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiefOccupants_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalSchema: "mas",
                        principalTable: "Districts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChiefOccupants_MohAreas_MohAreaId",
                        column: x => x.MohAreaId,
                        principalSchema: "mas",
                        principalTable: "MohAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChiefOccupants_PublicHealthInspectors_PhiId",
                        column: x => x.PhiId,
                        principalSchema: "moh",
                        principalTable: "PublicHealthInspectors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReportingApplications",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    ArchivedAt = table.Column<DateTime>(nullable: true),
                    ChiefOccupantId = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportingApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportingApplications_ChiefOccupants_ChiefOccupantId",
                        column: x => x.ChiefOccupantId,
                        principalSchema: "pub",
                        principalTable: "ChiefOccupants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Penalties",
                schema: "pub",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    ArchivedAt = table.Column<DateTime>(nullable: true),
                    ResolvedDate = table.Column<DateTime>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: false),
                    PenaltyType = table.Column<int>(nullable: false),
                    PenaltyStatus = table.Column<int>(nullable: false),
                    ChiefOccupantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Penalties_ChiefOccupants_ChiefOccupantId",
                        column: x => x.ChiefOccupantId,
                        principalSchema: "pub",
                        principalTable: "ChiefOccupants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SurroundingSets",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    ArchivedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    ReportingApplicationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurroundingSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurroundingSets_ReportingApplications_ReportingApplicationId",
                        column: x => x.ReportingApplicationId,
                        principalSchema: "app",
                        principalTable: "ReportingApplications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkItems",
                schema: "wkitm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    ArchivedAt = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    ChiefOccupantId = table.Column<int>(nullable: true),
                    ReportingApplicationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkItems_ChiefOccupants_ChiefOccupantId",
                        column: x => x.ChiefOccupantId,
                        principalSchema: "pub",
                        principalTable: "ChiefOccupants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkItems_ReportingApplications_ReportingApplicationId",
                        column: x => x.ReportingApplicationId,
                        principalSchema: "app",
                        principalTable: "ReportingApplications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkItemActions",
                schema: "wkitm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    ArchivedAt = table.Column<DateTime>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    AssignToId = table.Column<int>(nullable: true),
                    WorkItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItemActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkItemActions_PublicHealthInspectors_AssignToId",
                        column: x => x.AssignToId,
                        principalSchema: "moh",
                        principalTable: "PublicHealthInspectors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkItemActions_WorkItems_WorkItemId",
                        column: x => x.WorkItemId,
                        principalSchema: "wkitm",
                        principalTable: "WorkItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkItemRemarks",
                schema: "wkitm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    ArchivedAt = table.Column<DateTime>(nullable: true),
                    WorkItemId = table.Column<int>(nullable: false),
                    OwnerName = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItemRemarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkItemRemarks_WorkItems_WorkItemId",
                        column: x => x.WorkItemId,
                        principalSchema: "wkitm",
                        principalTable: "WorkItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportingApplications_ChiefOccupantId",
                schema: "app",
                table: "ReportingApplications",
                column: "ChiefOccupantId");

            migrationBuilder.CreateIndex(
                name: "IX_SurroundingSets_ReportingApplicationId",
                schema: "app",
                table: "SurroundingSets",
                column: "ReportingApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiefOccupants_DistrictId",
                schema: "pub",
                table: "ChiefOccupants",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiefOccupants_MohAreaId",
                schema: "pub",
                table: "ChiefOccupants",
                column: "MohAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiefOccupants_PhiId",
                schema: "pub",
                table: "ChiefOccupants",
                column: "PhiId");

            migrationBuilder.CreateIndex(
                name: "IX_Penalties_ChiefOccupantId",
                schema: "pub",
                table: "Penalties",
                column: "ChiefOccupantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItemActions_AssignToId",
                schema: "wkitm",
                table: "WorkItemActions",
                column: "AssignToId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItemActions_WorkItemId",
                schema: "wkitm",
                table: "WorkItemActions",
                column: "WorkItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItemRemarks_WorkItemId",
                schema: "wkitm",
                table: "WorkItemRemarks",
                column: "WorkItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_ChiefOccupantId",
                schema: "wkitm",
                table: "WorkItems",
                column: "ChiefOccupantId",
                unique: true,
                filter: "[ChiefOccupantId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_ReportingApplicationId",
                schema: "wkitm",
                table: "WorkItems",
                column: "ReportingApplicationId",
                unique: true,
                filter: "[ReportingApplicationId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurroundingSets",
                schema: "app");

            migrationBuilder.DropTable(
                name: "Penalties",
                schema: "pub");

            migrationBuilder.DropTable(
                name: "WorkItemActions",
                schema: "wkitm");

            migrationBuilder.DropTable(
                name: "WorkItemRemarks",
                schema: "wkitm");

            migrationBuilder.DropTable(
                name: "WorkItems",
                schema: "wkitm");

            migrationBuilder.DropTable(
                name: "ReportingApplications",
                schema: "app");

            migrationBuilder.DropTable(
                name: "ChiefOccupants",
                schema: "pub");
        }
    }
}
