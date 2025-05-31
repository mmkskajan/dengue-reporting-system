using Microsoft.EntityFrameworkCore.Migrations;

namespace CIDRS.Infrastructure.Migrations
{
    public partial class MohAreaPolicestationSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                columns: new[] { "MohAreaId", "PoliceStationId" },
                values: new object[,]
                {
                    { 116, 1 },
                    { 115, 10 },
                    { 116, 10 },
                    { 109, 11 },
                    { 112, 11 },
                    { 116, 11 },
                    { 111, 12 },
                    { 111, 13 },
                    { 107, 14 },
                    { 115, 14 },
                    { 108, 15 },
                    { 112, 15 },
                    { 114, 15 },
                    { 112, 16 },
                    { 113, 16 },
                    { 109, 16 },
                    { 113, 9 },
                    { 108, 17 },
                    { 112, 9 },
                    { 114, 8 },
                    { 114, 1 },
                    { 109, 1 },
                    { 116, 2 },
                    { 115, 2 },
                    { 112, 3 },
                    { 109, 3 },
                    { 110, 3 },
                    { 114, 3 },
                    { 115, 4 },
                    { 116, 4 },
                    { 114, 5 },
                    { 114, 6 },
                    { 110, 6 },
                    { 114, 7 },
                    { 110, 8 },
                    { 109, 9 },
                    { 114, 17 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 107, 14 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 108, 15 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 108, 17 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 109, 1 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 109, 3 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 109, 9 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 109, 11 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 109, 16 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 110, 3 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 110, 6 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 110, 8 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 111, 12 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 111, 13 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 112, 3 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 112, 9 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 112, 11 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 112, 15 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 112, 16 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 113, 9 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 113, 16 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 114, 1 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 114, 3 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 114, 5 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 114, 6 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 114, 7 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 114, 8 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 114, 15 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 114, 17 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 115, 2 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 115, 4 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 115, 10 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 115, 14 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 116, 1 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 116, 2 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 116, 4 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 116, 10 });

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreaPoliceStations",
                keyColumns: new[] { "MohAreaId", "PoliceStationId" },
                keyValues: new object[] { 116, 11 });
        }
    }
}
