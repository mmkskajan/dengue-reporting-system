using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CIDRS.Infrastructure.Migrations
{
    public partial class Seed_MOHArea_Districts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PoliceStations_MohAreas_MohAreaId",
                schema: "mas",
                table: "PoliceStations");

            migrationBuilder.DropIndex(
                name: "IX_PoliceStations_MohAreaId",
                schema: "mas",
                table: "PoliceStations");

            migrationBuilder.DropColumn(
                name: "MohAreaId",
                schema: "mas",
                table: "PoliceStations");

            migrationBuilder.CreateTable(
                name: "MohAreaPoliceStations",
                schema: "mas",
                columns: table => new
                {
                    MohAreaId = table.Column<int>(nullable: false),
                    PoliceStationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MohAreaPoliceStations", x => new { x.MohAreaId, x.PoliceStationId });
                    table.ForeignKey(
                        name: "FK_MohAreaPoliceStations_MohAreas_MohAreaId",
                        column: x => x.MohAreaId,
                        principalSchema: "mas",
                        principalTable: "MohAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MohAreaPoliceStations_PoliceStations_PoliceStationId",
                        column: x => x.PoliceStationId,
                        principalSchema: "mas",
                        principalTable: "PoliceStations",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "mas",
                table: "Districts",
                columns: new[] { "Id", "ArchivedAt", "CreatedAt", "Identifier", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(829), "D0000001", "Ampara", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8221) },
                    { 23, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(9032), "D0000023", "Ratnapura", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(9033) },
                    { 22, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(9028), "D0000022", "Puttalam", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(9029) },
                    { 21, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(9024), "D0000021", "Polonnaruwa", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(9025) },
                    { 20, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(9017), "D0000020", "Nuwara Eliya	", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(9019) },
                    { 19, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8930), "D0000019", "Mullaitivu", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8931) },
                    { 18, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8926), "D0000018", "Monaragala", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8927) },
                    { 17, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8922), "D0000017", "Matara", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8923) },
                    { 16, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8918), "D0000016", "Matale", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8919) },
                    { 15, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8914), "D0000015", "Mannar", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8915) },
                    { 14, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8910), "D0000014", "Kurunegala", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8911) },
                    { 24, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(9035), "D0000024", "Trincomalee", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(9036) },
                    { 13, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8907), "D0000013", "Kilinochchi", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8908) },
                    { 11, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8898), "D0000011", "Kandy", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8899) },
                    { 10, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8894), "D0000010", "Kalutara", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8895) },
                    { 9, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8890), "D0000009", "Jaffna", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8891) },
                    { 8, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8885), "D0000008", "Hambantota", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8887) },
                    { 7, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8881), "D0000007", "Gampaha", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8882) },
                    { 6, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8877), "D0000006", "Galle", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8878) },
                    { 5, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8873), "D0000005", "Colombo", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8874) },
                    { 4, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8869), "D0000004", "Batticaloa", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8870) },
                    { 3, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8865), "D0000003", "Badulla", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8866) },
                    { 2, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8836), "D0000002", "Anuradhapura", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8848) },
                    { 12, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8903), "D0000012", "Kegalle", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(8904) },
                    { 25, null, new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(9039), "D0000025", "Vavuniya", new DateTime(2021, 6, 13, 18, 21, 27, 658, DateTimeKind.Local).AddTicks(9040) }
                });

            migrationBuilder.InsertData(
                schema: "mas",
                table: "MohAreas",
                columns: new[] { "Id", "ArchivedAt", "CreatedAt", "DistrictId", "Identifier", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(1745), 1, "MOH0000001", "Ampara", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(1773) },
                    { 219, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3366), 15, "MOH0000219", "Adampan", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3367) },
                    { 197, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3247), 14, "MOH0000197", "Wariyapola", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3248) },
                    { 196, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3243), 14, "MOH0000196", "Udubaddawa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3245) },
                    { 195, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3240), 14, "MOH0000195", "Rideegama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3241) },
                    { 194, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3236), 14, "MOH0000194", "Polpitigama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3237) },
                    { 193, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3232), 14, "MOH0000193", "Polgahawela", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3233) },
                    { 220, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3370), 15, "MOH0000220", "Madhu", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3371) },
                    { 192, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3228), 14, "MOH0000192", "Pannala", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3229) },
                    { 190, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3221), 14, "MOH0000190", "Nikaweratiya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3222) },
                    { 189, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3217), 14, "MOH0000189", "Narammala", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3218) },
                    { 188, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3213), 14, "MOH0000188", "Mawathagama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3214) },
                    { 187, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3208), 14, "MOH0000187", "Maho", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3210) },
                    { 186, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3170), 14, "MOH0000186", "Kurunegala", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3171) },
                    { 185, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3166), 14, "MOH0000185", "Kuliyapitiya West", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3167) },
                    { 191, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3225), 14, "MOH0000191", "Panduwasnuwara", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3226) },
                    { 184, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3162), 14, "MOH0000184", "Kotawehera", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3163) },
                    { 221, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3374), 15, "MOH0000221", "Mannar", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3375) },
                    { 223, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3381), 15, "MOH0000223", "Musali", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3382) },
                    { 227, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3396), 17, "MOH0000227", "Dickwella", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3397) },
                    { 226, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3392), 17, "MOH0000226", "Devinuara", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3393) },
                    { 225, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3388), 17, "MOH0000225", "Athuraliya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3389) },
                    { 224, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3385), 17, "MOH0000224", "Akuressa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3386) },
                    { 207, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3285), 16, "MOH0000207", "Yatawatta", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3286) },
                    { 206, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3281), 16, "MOH0000206", "Wilgamuwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3282) },
                    { 222, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3378), 15, "MOH0000222", "Murunkan", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3378) },
                    { 205, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3277), 16, "MOH0000205", "Ukuwela", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3278) },
                    { 203, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3270), 16, "MOH0000203", "Naula", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3271) },
                    { 202, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3266), 16, "MOH0000202", "Matale", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3267) },
                    { 201, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3262), 16, "MOH0000201", "Laggala / Pallegama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3263) },
                    { 200, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3259), 16, "MOH0000200", "Galewela", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3260) },
                    { 199, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3255), 16, "MOH0000199", "Dambulla", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3256) },
                    { 198, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3251), 16, "MOH0000198", "Abanganga", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3252) },
                    { 204, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3274), 16, "MOH0000204", "Pallepola", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3275) },
                    { 183, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3158), 14, "MOH0000183", "Katupotha", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3159) },
                    { 182, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3154), 14, "MOH0000182", "Ibbagamuwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3155) },
                    { 181, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3150), 14, "MOH0000181", "Giribawa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3151) },
                    { 150, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2995), 11, "MOH0000150", "Waththegama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2995) },
                    { 149, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2991), 11, "MOH0000149", "Udunuwara", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2992) },
                    { 148, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2987), 11, "MOH0000148", "Udapalatha Gampala", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2988) },
                    { 147, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2983), 11, "MOH0000147", "Udadumbara", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2984) },
                    { 146, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2979), 11, "MOH0000146", "Poojapitiya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2980) },
                    { 145, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2976), 11, "MOH0000145", "Thalatuoya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2977) },
                    { 151, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2998), 11, "MOH0000151", "Yatinuwara", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3000) },
                    { 144, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2972), 11, "MOH0000144", "Panvila", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2973) },
                    { 142, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2964), 11, "MOH0000142", "Medadumbara", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2965) },
                    { 141, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2960), 11, "MOH0000141", "Manikhinna", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2961) },
                    { 140, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2957), 11, "MOH0000140", "Kundasale", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2958) },
                    { 139, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2952), 11, "MOH0000139", "Kadugannawa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2953) },
                    { 138, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2948), 11, "MOH0000138", "Hatharaliyadda", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2949) },
                    { 137, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2944), 11, "MOH0000137", "Hasalaka", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2945) },
                    { 143, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2968), 11, "MOH0000143", "Nawalapitiya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2969) },
                    { 163, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3081), 12, "MOH0000163", "Aranayake", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3082) },
                    { 164, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3085), 12, "MOH0000164", "Bulathkohupitiya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3086) },
                    { 165, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3089), 12, "MOH0000165", "Dehiovita", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3090) },
                    { 180, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3147), 14, "MOH0000180", "Ganewatta", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3147) },
                    { 179, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3143), 14, "MOH0000179", "Galgamuwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3144) },
                    { 178, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3139), 14, "MOH0000178", "Bingiriya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3140) },
                    { 177, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3135), 14, "MOH0000177", "Alauwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3136) },
                    { 176, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3131), 13, "MOH0000176", "Poonakary", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3132) },
                    { 175, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3128), 13, "MOH0000175", "Pallai", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3129) },
                    { 174, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3124), 13, "MOH0000174", "Kilinochchi", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3125) },
                    { 173, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3120), 12, "MOH0000173", "Yatiyantota", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3121) },
                    { 172, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3116), 12, "MOH0000172", "Warakapola", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3117) },
                    { 171, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3112), 12, "MOH0000171", "Ruwanwella", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3113) },
                    { 170, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3108), 12, "MOH0000170", "Rambukkana", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3109) },
                    { 169, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3104), 12, "MOH0000169", "Mawanella", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3105) },
                    { 168, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3100), 12, "MOH0000168", "Kegalle", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3101) },
                    { 167, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3096), 12, "MOH0000167", "Galigamuwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3097) },
                    { 166, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3092), 12, "MOH0000166", "Deraniyagala", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3094) },
                    { 228, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3400), 17, "MOH0000228", "Hakmana", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3401) },
                    { 136, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2941), 11, "MOH0000136", "Harispattuwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2942) },
                    { 229, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3403), 17, "MOH0000229", "Kamburupitiya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3404) },
                    { 231, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3411), 17, "MOH0000231", "Kotapola", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3412) },
                    { 271, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3681), 23, "MOH0000271", "Balangoda", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3682) },
                    { 270, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3677), 23, "MOH0000270", "Ayagama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3678) },
                    { 269, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3673), 22, "MOH0000269", "Puttalam", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3674) },
                    { 268, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3670), 22, "MOH0000268", "Mundal", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3671) },
                    { 267, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3666), 22, "MOH0000267", "Marawila", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3667) },
                    { 266, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3662), 22, "MOH0000266", "Karuwalagaswewa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3663) },
                    { 272, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3684), 23, "MOH0000272", "Eheliyagoda", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3685) },
                    { 265, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3600), 22, "MOH0000265", "Kalpitiya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3601) },
                    { 263, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3592), 22, "MOH0000263", "Chillaw", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3593) },
                    { 262, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3589), 22, "MOH0000262", "Arachchikattuwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3589) },
                    { 261, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3585), 22, "MOH0000261", "Anamaduwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3586) },
                    { 260, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3581), 21, "MOH0000260", "Walikanda", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3582) },
                    { 259, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3577), 21, "MOH0000259", "Thamankaduwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3578) },
                    { 258, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3574), 21, "MOH0000258", "Medirigiriya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3575) },
                    { 264, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3596), 22, "MOH0000264", "Dankotuwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3597) },
                    { 257, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3570), 21, "MOH0000257", "Lankapura", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3571) },
                    { 273, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3688), 23, "MOH0000273", "Elapatha", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3689) },
                    { 275, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3696), 23, "MOH0000275", "Kolonna", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3697) },
                    { 289, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3748), 25, "MOH0000289", "Cheddikulam", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3749) },
                    { 288, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3744), 25, "MOH0000288", "Vavuniya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3745) },
                    { 287, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3741), 24, "MOH0000287", "Trincomalee", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3742) },
                    { 286, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3737), 24, "MOH0000286", "Thampalagamam", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3738) },
                    { 285, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3733), 24, "MOH0000285", "Seruwila", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3734) },
                    { 284, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3730), 24, "MOH0000284", "Padavi Sripura", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3731) },
                    { 274, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3692), 23, "MOH0000274", "Kiriella", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3693) },
                    { 283, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3726), 24, "MOH0000283", "Muthur", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3727) },
                    { 281, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3718), 24, "MOH0000281", "Kinniya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3719) },
                    { 280, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3714), 24, "MOH0000280", "Kanthali", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3715) },
                    { 279, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3710), 24, "MOH0000279", "Ichchilampathai", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3711) },
                    { 278, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3707), 24, "MOH0000278", "Gomarangadawela", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3708) },
                    { 277, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3703), 23, "MOH0000277", "Nivithigala", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3704) },
                    { 276, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3700), 23, "MOH0000276", "Kuruwita", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3701) },
                    { 282, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3722), 24, "MOH0000282", "Kuchchaveli", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3723) },
                    { 256, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3566), 21, "MOH0000256", "Hingurakgoda", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3567) },
                    { 255, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3562), 21, "MOH0000255", "Elehara", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3563) },
                    { 254, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3559), 21, "MOH0000254", "Dimbulagala", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3559) },
                    { 213, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3308), 18, "MOH0000213", "Madulla", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3309) },
                    { 212, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3304), 18, "MOH0000212", "Madagama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3305) },
                    { 211, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3300), 18, "MOH0000211", "Katharagama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3301) },
                    { 210, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3296), 18, "MOH0000210", "Buttala", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3297) },
                    { 209, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3292), 18, "MOH0000209", "Bibile", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3293) },
                    { 208, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3288), 18, "MOH0000208", "Badulkumbura", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3289) },
                    { 214, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3312), 18, "MOH0000214", "Monaragala", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3313) },
                    { 239, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3441), 17, "MOH0000239", "Welinitiya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3442) },
                    { 237, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3433), 17, "MOH0000237", "Thihagoda", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3434) },
                    { 236, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3429), 17, "MOH0000236", "Pasgoda", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3430) },
                    { 235, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3425), 17, "MOH0000235", "Mulatiyana", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3426) },
                    { 234, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3422), 17, "MOH0000234", "Morawaka", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3423) },
                    { 233, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3418), 17, "MOH0000233", "Matara", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3419) },
                    { 232, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3414), 17, "MOH0000232", "Malimbada", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3415) },
                    { 238, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3437), 17, "MOH0000238", "Weligama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3438) },
                    { 215, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3315), 18, "MOH0000215", "Sewanagala", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3316) },
                    { 216, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3319), 18, "MOH0000216", "Siyambalanduwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3320) },
                    { 217, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3323), 18, "MOH0000217", "Thanamalwila", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3324) },
                    { 253, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3555), 20, "MOH0000253", "Walapane", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3556) },
                    { 252, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3551), 20, "MOH0000252", "Thalawakela(Lindula)", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3552) },
                    { 251, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3547), 20, "MOH0000251", "Rikillagaskada", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3548) },
                    { 250, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3481), 20, "MOH0000250", "Nuwara - Eliya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3482) },
                    { 249, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3477), 20, "MOH0000249", "Nawathispane", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3478) },
                    { 248, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3473), 20, "MOH0000248", "Maskeliya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3474) },
                    { 247, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3470), 20, "MOH0000247", "Kothmale", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3471) },
                    { 246, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3466), 20, "MOH0000246", "Kotagala", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3467) },
                    { 245, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3463), 20, "MOH0000245", "Hanguranketha", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3464) },
                    { 244, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3459), 20, "MOH0000244", "Ambagamuwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3460) },
                    { 243, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3455), 19, "MOH0000243", "Puthukudyirrupu", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3456) },
                    { 242, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3452), 19, "MOH0000242", "Oddusuddan", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3453) },
                    { 241, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3448), 19, "MOH0000241", "Mullaitivu", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3449) },
                    { 240, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3444), 19, "MOH0000240", "Mallavi", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3445) },
                    { 218, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3328), 18, "MOH0000218", "Wellawaya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3329) },
                    { 230, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3407), 17, "MOH0000230", "Kirinda/Puhulwella", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3408) },
                    { 135, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2937), 11, "MOH0000135", "Gangawatakorale / Tennekumbura", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2938) },
                    { 134, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2933), 11, "MOH0000134", "Ganga Ihala Korale", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2934) },
                    { 133, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2929), 11, "MOH0000133", "Galaha", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2930) },
                    { 39, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2445), 4, "MOH0000039", "Eravur", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2446) },
                    { 38, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2441), 4, "MOH0000038", "Chenkalady", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2442) },
                    { 37, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2437), 4, "MOH0000037", "Batticalo", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2438) },
                    { 292, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2432), 4, "MOH0000292", "Arayampathy", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2433) },
                    { 36, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2428), 3, "MOH0000036", "Welimada", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2429) },
                    { 35, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2424), 3, "MOH0000035", "Passara", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2425) },
                    { 40, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2449), 4, "MOH0000040", "Kaluwanchikudy", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2450) },
                    { 34, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2420), 3, "MOH0000034", "Paranagama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2421) },
                    { 32, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2411), 3, "MOH0000032", "Kandaketiya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2412) },
                    { 31, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2407), 3, "MOH0000031", "Haputale", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2408) },
                    { 30, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2404), 3, "MOH0000030", "Hali Ela", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2404) },
                    { 29, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2400), 3, "MOH0000029", "Girandurukotte", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2401) },
                    { 28, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2394), 3, "MOH0000028", "Ella", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2396) },
                    { 27, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2343), 3, "MOH0000027", "Bandarawela", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2344) },
                    { 33, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2416), 3, "MOH0000033", "Mahiyangana", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2417) },
                    { 26, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2340), 3, "MOH0000026", "Badulla", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2341) },
                    { 41, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2453), 4, "MOH0000041", "Kattankudy", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2454) },
                    { 43, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2460), 4, "MOH0000043", "Paddippalai", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2461) },
                    { 57, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2516), 5, "MOH0000057", "Padukka", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2517) },
                    { 56, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2513), 5, "MOH0000056", "Nugegoda", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2514) },
                    { 55, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2509), 5, "MOH0000055", "Moratuwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2510) },
                    { 54, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2504), 5, "MOH0000054", "Maharagama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2506) },
                    { 53, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2501), 5, "MOH0000053", "Kolonnawa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2501) },
                    { 52, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2496), 5, "MOH0000052", "Kaduwela", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2497) },
                    { 42, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2457), 4, "MOH0000042", "Odamavadi", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2457) },
                    { 51, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2492), 5, "MOH0000051", "Homagama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2493) },
                    { 49, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2484), 5, "MOH0000049", "Dehiwala", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2485) },
                    { 48, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2480), 5, "MOH0000048", "Boralesgamuwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2481) },
                    { 47, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2476), 4, "MOH0000047", "Vellavely", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2477) },
                    { 46, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2472), 4, "MOH0000046", "Vavunathevu", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2473) },
                    { 45, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2468), 4, "MOH0000045", "Valaichchenai", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2469) },
                    { 44, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2464), 4, "MOH0000044", "Vakarai", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2465) },
                    { 50, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2488), 5, "MOH0000050", "Hanwella", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2489) },
                    { 25, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2336), 2, "MOH0000025", "Thirappane", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2337) },
                    { 24, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2332), 2, "MOH0000024", "Thalawa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2333) },
                    { 23, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2328), 2, "MOH0000023", "Thabuththegama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2329) },
                    { 125, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2896), 1, "MOH0000125", "Potthuvil", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2897) },
                    { 124, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2891), 1, "MOH0000124", "Nintavur", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2892) },
                    { 123, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2887), 1, "MOH0000123", "Karaitivu", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2888) },
                    { 122, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2849), 1, "MOH0000122", "Kalmunai South", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2850) },
                    { 121, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2845), 1, "MOH0000121", "Kalmunai North", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2846) },
                    { 120, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2841), 1, "MOH0000120", "Irkkamam", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2842) },
                    { 126, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2900), 1, "MOH0000126", "Sainthamaruthu", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2901) },
                    { 119, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2837), 1, "MOH0000119", "Alayadivembu", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2838) },
                    { 117, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2830), 1, "MOH0000117", "Adalaichchenai", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2831) },
                    { 6, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2259), 1, "MOH0000006", "Uhana", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2260) },
                    { 5, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2255), 1, "MOH0000005", "Padiyathalawa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2256) },
                    { 4, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2251), 1, "MOH0000004", "Mahaoya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2252) },
                    { 3, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2246), 1, "MOH0000003", "Lahugala", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2248) },
                    { 2, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2230), 1, "MOH0000002", "Damana", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2234) },
                    { 118, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2834), 1, "MOH0000118", "Akkaraipatthu", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2835) },
                    { 127, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2904), 1, "MOH0000127", "Sammanthurai", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2905) },
                    { 128, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2909), 1, "MOH0000128", "Thirukkovil", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2910) },
                    { 7, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2263), 2, "MOH0000007", "Galinbidinuwewa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2264) },
                    { 22, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2324), 2, "MOH0000022", "Rambewa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2325) },
                    { 21, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2320), 2, "MOH0000021", "Rajanganaya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2321) },
                    { 20, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2316), 2, "MOH0000020", "Palagala", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2317) },
                    { 19, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2312), 2, "MOH0000019", "Padaviya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2313) },
                    { 18, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2309), 2, "MOH0000018", "Nuwaragam Palatha - East", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2310) },
                    { 17, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2304), 2, "MOH0000017", "Nuwaragam Palatha - Central", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2306) },
                    { 16, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2301), 2, "MOH0000016", "Nochchiyagama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2302) },
                    { 15, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2297), 2, "MOH0000015", "Mihintale", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2298) },
                    { 14, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2293), 2, "MOH0000014", "Madawachchiya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2294) },
                    { 13, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2289), 2, "MOH0000013", "Kekirawa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2290) },
                    { 12, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2285), 2, "MOH0000012", "Kebithigollewa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2286) },
                    { 11, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2279), 2, "MOH0000011", "Kahatagasdigililiya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2280) },
                    { 10, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2275), 2, "MOH0000010", "Ipalogama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2276) },
                    { 9, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2271), 2, "MOH0000009", "Horowpothana", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2272) },
                    { 8, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2267), 2, "MOH0000008", "Galnewa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2268) },
                    { 58, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2522), 5, "MOH0000058", "Piliyandala", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2523) },
                    { 59, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2561), 5, "MOH0000059", "Pittakotte", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2562) },
                    { 60, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2566), 6, "MOH0000060", "Ambalangoda", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2567) },
                    { 61, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2570), 6, "MOH0000061", "Akmeemana", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2571) },
                    { 112, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2811), 9, "MOH0000112", "Kopay", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2812) },
                    { 111, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2807), 9, "MOH0000111", "Kayts", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2808) },
                    { 110, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2803), 9, "MOH0000110", "Karaveddy", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2804) },
                    { 109, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2800), 9, "MOH0000109", "Jaffna", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2801) },
                    { 108, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2796), 9, "MOH0000108", "Chawacachcheri", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2797) },
                    { 107, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2792), 9, "MOH0000107", "Chankanai", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2793) },
                    { 113, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2815), 9, "MOH0000113", "Nallur", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2816) },
                    { 106, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2788), 8, "MOH0000106", "Weerakatiya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2789) },
                    { 104, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2780), 8, "MOH0000104", "Tissamaharama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2781) },
                    { 103, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2776), 8, "MOH0000103", "Tangalle", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2778) },
                    { 102, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2773), 8, "MOH0000102", "Sooriyawewa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2774) },
                    { 101, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2769), 8, "MOH0000101", "Okewela", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2770) },
                    { 100, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2765), 8, "MOH0000100", "Lunugamvehera", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2766) },
                    { 99, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2761), 8, "MOH0000099", "Katuwana", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2762) },
                    { 105, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2785), 8, "MOH0000105", "Walasmulla", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2786) },
                    { 114, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2819), 9, "MOH0000114", "Point Pedro", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2820) },
                    { 115, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2822), 9, "MOH0000115", "Sandilipay", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2823) },
                    { 116, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2826), 9, "MOH0000116", "Uduvil", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2827) },
                    { 132, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2926), 11, "MOH0000132", "Galagedara", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2927) },
                    { 131, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2922), 11, "MOH0000131", "Doluwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2923) },
                    { 130, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2917), 11, "MOH0000130", "Bambaradeniya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2919) },
                    { 129, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2913), 11, "MOH0000129", "Akurana", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2914) },
                    { 162, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3077), 10, "MOH0000162", "Walallawita", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3078) },
                    { 161, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3073), 10, "MOH0000161", "Panadura", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3074) },
                    { 160, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3068), 10, "MOH0000160", "Palindanuwara", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3069) },
                    { 159, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3065), 10, "MOH0000159", "Mathugama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3066) },
                    { 158, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3060), 10, "MOH0000158", "Madurawala", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3061) },
                    { 157, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3057), 10, "MOH0000157", "Kalutara", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3058) },
                    { 156, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3053), 10, "MOH0000156", "Ingiriya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3054) },
                    { 155, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3049), 10, "MOH0000155", "Horana", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3050) },
                    { 154, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3010), 10, "MOH0000154", "Bulathsinhala", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3011) },
                    { 153, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3006), 10, "MOH0000153", "Bandaragama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3007) },
                    { 152, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3003), 10, "MOH0000152", "Agalawatta", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3004) },
                    { 98, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2757), 8, "MOH0000098", "Hambantota", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2758) },
                    { 290, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3752), 25, "MOH0000290", "Vavuniya North", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3753) },
                    { 97, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2753), 8, "MOH0000097", "Beliatta", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2754) },
                    { 95, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2746), 8, "MOH0000095", "Ambalanthota", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2747) },
                    { 75, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2628), 6, "MOH0000075", "Thawalama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2629) },
                    { 74, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2624), 6, "MOH0000074", "Niyagama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2625) },
                    { 73, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2620), 6, "MOH0000073", "Neluwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2621) },
                    { 72, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2616), 6, "MOH0000072", "Karandeniya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2617) },
                    { 71, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2612), 6, "MOH0000071", "Imaduwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2613) },
                    { 70, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2608), 6, "MOH0000070", "Induruwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2609) },
                    { 76, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2632), 6, "MOH0000076", "Udugama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2633) },
                    { 69, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2605), 6, "MOH0000069", "Hikkaduwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2606) },
                    { 67, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2597), 6, "MOH0000067", "Gonapeenuwala", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2598) },
                    { 66, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2591), 6, "MOH0000066", "Galle MC", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2592) },
                    { 65, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2586), 6, "MOH0000065", "Elpitiya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2587) },
                    { 64, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2582), 6, "MOH0000064", "Bope - Poddala", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2583) },
                    { 63, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2578), 6, "MOH0000063", "Balapitiya / Ahungalle", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2579) },
                    { 62, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2573), 6, "MOH0000062", "Baddegama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2575) },
                    { 68, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2601), 6, "MOH0000068", "Habaraduwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2602) },
                    { 77, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2635), 6, "MOH0000077", "Welivitiya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2636) },
                    { 78, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2639), 6, "MOH0000078", "Yakkalamulla", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2640) },
                    { 79, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2643), 7, "MOH0000079", "Attanagalla", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2644) },
                    { 94, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2742), 7, "MOH0000094", "Wattala", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2743) },
                    { 93, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2737), 7, "MOH0000093", "Seeduwa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2738) },
                    { 92, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2733), 7, "MOH0000092", "Ragama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2734) },
                    { 91, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2729), 7, "MOH0000091", "Negombo", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2730) },
                    { 90, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2689), 7, "MOH0000090", "Mirigama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2690) },
                    { 89, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2685), 7, "MOH0000089", "Minuwangoda", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2686) },
                    { 88, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2681), 7, "MOH0000088", "Mahara", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2682) },
                    { 87, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2677), 7, "MOH0000087", "Kelaniya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2678) },
                    { 86, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2671), 7, "MOH0000086", "Katana", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2674) },
                    { 85, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2667), 7, "MOH0000085", "Jaela", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2668) },
                    { 84, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2662), 7, "MOH0000084", "Gampaha", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2664) },
                    { 83, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2659), 7, "MOH0000083", "Dompe", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2660) },
                    { 82, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2655), 7, "MOH0000082", "Divulapitiya", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2656) },
                    { 81, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2651), 7, "MOH0000081", "BOI - Katunayaka", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2652) },
                    { 80, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2647), 7, "MOH0000080", "Biyagama", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2648) },
                    { 96, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2749), 8, "MOH0000096", "Angunakolapalessa", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(2750) },
                    { 291, null, new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3756), 25, "MOH0000291", "Vavuniya South", new DateTime(2021, 6, 13, 18, 21, 27, 685, DateTimeKind.Local).AddTicks(3757) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MohAreaPoliceStations_PoliceStationId",
                schema: "mas",
                table: "MohAreaPoliceStations",
                column: "PoliceStationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MohAreaPoliceStations",
                schema: "mas");

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 250);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 251);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 252);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 253);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 254);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 255);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 256);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 257);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 258);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 259);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 260);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 261);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 262);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 263);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 264);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 265);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 266);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 267);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 268);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 269);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 270);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 271);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 272);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 273);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 274);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 275);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 276);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 277);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 278);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 279);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 280);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 281);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 282);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 283);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 284);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 285);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 286);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 287);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 288);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 289);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 290);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 291);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "MohAreas",
                keyColumn: "Id",
                keyValue: 292);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "mas",
                table: "Districts",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.AddColumn<int>(
                name: "MohAreaId",
                schema: "mas",
                table: "PoliceStations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PoliceStations_MohAreaId",
                schema: "mas",
                table: "PoliceStations",
                column: "MohAreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PoliceStations_MohAreas_MohAreaId",
                schema: "mas",
                table: "PoliceStations",
                column: "MohAreaId",
                principalSchema: "mas",
                principalTable: "MohAreas",
                principalColumn: "Id");
        }
    }
}
