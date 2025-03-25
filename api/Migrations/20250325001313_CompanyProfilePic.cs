using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class CompanyProfilePic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4d490e44-9cd0-4923-bab5-2122a3d9d627", null, "JobSeeker", "JOBSEEKER" },
                    { "af96f76f-651c-4837-8cca-8f52fc1240c5", null, "Admin", "ADMIN" },
                    { "f17f972f-da3d-4104-bccd-4c853efcb5e5", null, "Employer", "EMPLOYER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d490e44-9cd0-4923-bab5-2122a3d9d627");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af96f76f-651c-4837-8cca-8f52fc1240c5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f17f972f-da3d-4104-bccd-4c853efcb5e5");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Companies");

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
    }
}
