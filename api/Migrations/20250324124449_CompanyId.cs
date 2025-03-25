using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class CompanyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f37e20e-aeb5-410f-87fd-110ebbc00260");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "902b300d-7582-43a3-bfcc-ece7598a223f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac5d7f40-7223-403f-b588-b95024df0128");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Vacants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0da70770-e04f-49b3-a0bc-0336058e4891", null, "Employer", "EMPLOYER" },
                    { "c52efa39-0ec5-4851-853a-3804674f812d", null, "Admin", "ADMIN" },
                    { "d9f1cb2d-5093-46cc-9280-fa705378b33c", null, "JobSeeker", "JOBSEEKER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0da70770-e04f-49b3-a0bc-0336058e4891");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c52efa39-0ec5-4851-853a-3804674f812d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9f1cb2d-5093-46cc-9280-fa705378b33c");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Vacants");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f37e20e-aeb5-410f-87fd-110ebbc00260", null, "JobSeeker", "JOBSEEKER" },
                    { "902b300d-7582-43a3-bfcc-ece7598a223f", null, "Employer", "EMPLOYER" },
                    { "ac5d7f40-7223-403f-b588-b95024df0128", null, "Admin", "ADMIN" }
                });
        }
    }
}
