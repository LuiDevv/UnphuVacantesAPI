using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class onetoone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Portfolios_PortfolioAppUserId_PortfolioCompanyId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_PortfolioAppUserId_PortfolioCompanyId",
                table: "Companies");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d46d287-68bd-4757-aa4c-e9ff65078203");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98ea7dcf-4c7b-487a-ada6-e9b6dc2a8156");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd460d78-e521-4546-8db1-1dea2c1bcdae");

            migrationBuilder.DropColumn(
                name: "PortfolioAppUserId",
                table: "Companies");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11ddacba-7a0c-4b6a-b84d-8865a31a656b", null, "Employer", "EMPLOYER" },
                    { "1fbcadeb-ef36-40e8-8e7f-eb718ebe00db", null, "JobSeeker", "JOBSEEKER" },
                    { "dc30f935-e7b0-449d-a019-b2bb599fec09", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11ddacba-7a0c-4b6a-b84d-8865a31a656b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1fbcadeb-ef36-40e8-8e7f-eb718ebe00db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc30f935-e7b0-449d-a019-b2bb599fec09");

            migrationBuilder.AddColumn<string>(
                name: "PortfolioAppUserId",
                table: "Companies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5d46d287-68bd-4757-aa4c-e9ff65078203", null, "Employer", "EMPLOYER" },
                    { "98ea7dcf-4c7b-487a-ada6-e9b6dc2a8156", null, "Admin", "ADMIN" },
                    { "cd460d78-e521-4546-8db1-1dea2c1bcdae", null, "JobSeeker", "JOBSEEKER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_PortfolioAppUserId_PortfolioCompanyId",
                table: "Companies",
                columns: new[] { "PortfolioAppUserId", "PortfolioCompanyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Portfolios_PortfolioAppUserId_PortfolioCompanyId",
                table: "Companies",
                columns: new[] { "PortfolioAppUserId", "PortfolioCompanyId" },
                principalTable: "Portfolios",
                principalColumns: new[] { "AppUserId", "CompanyId" });
        }
    }
}
