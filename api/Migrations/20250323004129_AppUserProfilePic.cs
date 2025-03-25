using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AppUserProfilePic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12e6dda0-53aa-44a1-8066-bdf3ee789029");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "194ab15d-03cb-4fa8-8be7-e842881bc813");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60f5b66a-1871-4c12-b92f-74c600cc55d4");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "12e6dda0-53aa-44a1-8066-bdf3ee789029", null, "Admin", "ADMIN" },
                    { "194ab15d-03cb-4fa8-8be7-e842881bc813", null, "JobSeeker", "JOBSEEKER" },
                    { "60f5b66a-1871-4c12-b92f-74c600cc55d4", null, "Employer", "EMPLOYER" }
                });
        }
    }
}
