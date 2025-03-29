using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class SaveVacant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0df572b1-804c-475e-b9c8-66d7b813168f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ed7983f-bf39-4f69-8deb-044c5b12f0e7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "479e3fb3-ffec-4307-8a75-3171c92c5103");

            migrationBuilder.CreateTable(
                name: "SavedVacants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VacantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedVacants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedVacants_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavedVacants_Vacants_VacantId",
                        column: x => x.VacantId,
                        principalTable: "Vacants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "289fe01a-43df-46c7-bc64-397a6e94b3f4", null, "Employer", "EMPLOYER" },
                    { "ea2ea3e2-acf8-4687-8784-2c6f296bce5d", null, "Admin", "ADMIN" },
                    { "f1bf6ca1-0ed3-416b-91d9-222951738550", null, "JobSeeker", "JOBSEEKER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavedVacants_AppUserId",
                table: "SavedVacants",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedVacants_VacantId",
                table: "SavedVacants",
                column: "VacantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedVacants");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "289fe01a-43df-46c7-bc64-397a6e94b3f4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea2ea3e2-acf8-4687-8784-2c6f296bce5d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1bf6ca1-0ed3-416b-91d9-222951738550");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0df572b1-804c-475e-b9c8-66d7b813168f", null, "JobSeeker", "JOBSEEKER" },
                    { "1ed7983f-bf39-4f69-8deb-044c5b12f0e7", null, "Employer", "EMPLOYER" },
                    { "479e3fb3-ffec-4307-8a75-3171c92c5103", null, "Admin", "ADMIN" }
                });
        }
    }
}
