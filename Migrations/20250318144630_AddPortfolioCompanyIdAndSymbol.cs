using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddPortfolioCompanyIdAndSymbol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Portfolios_PortfolioAppUserId_PortfolioId",
                table: "Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Portfolios",
                table: "Portfolios");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1115db12-ec56-4896-8a4b-523d185ca552");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34d47177-1a33-494e-97cc-65e52d0f2124");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59058877-9fd6-4e4e-babe-f3ba0039c914");

            migrationBuilder.RenameColumn(
                name: "PortfolioId",
                table: "Companies",
                newName: "PortfolioCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_PortfolioAppUserId_PortfolioId",
                table: "Companies",
                newName: "IX_Companies_PortfolioAppUserId_PortfolioCompanyId");

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Portfolios",
                table: "Portfolios",
                columns: new[] { "AppUserId", "CompanyId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5d46d287-68bd-4757-aa4c-e9ff65078203", null, "Employer", "EMPLOYER" },
                    { "98ea7dcf-4c7b-487a-ada6-e9b6dc2a8156", null, "Admin", "ADMIN" },
                    { "cd460d78-e521-4546-8db1-1dea2c1bcdae", null, "JobSeeker", "JOBSEEKER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Portfolios_PortfolioAppUserId_PortfolioCompanyId",
                table: "Companies",
                columns: new[] { "PortfolioAppUserId", "PortfolioCompanyId" },
                principalTable: "Portfolios",
                principalColumns: new[] { "AppUserId", "CompanyId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Portfolios_PortfolioAppUserId_PortfolioCompanyId",
                table: "Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Portfolios",
                table: "Portfolios");

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
                name: "Symbol",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "PortfolioCompanyId",
                table: "Companies",
                newName: "PortfolioId");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_PortfolioAppUserId_PortfolioCompanyId",
                table: "Companies",
                newName: "IX_Companies_PortfolioAppUserId_PortfolioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Portfolios",
                table: "Portfolios",
                columns: new[] { "AppUserId", "Id" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1115db12-ec56-4896-8a4b-523d185ca552", null, "Employer", "EMPLOYER" },
                    { "34d47177-1a33-494e-97cc-65e52d0f2124", null, "Admin", "ADMIN" },
                    { "59058877-9fd6-4e4e-babe-f3ba0039c914", null, "JobSeeker", "JOBSEEKER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Portfolios_PortfolioAppUserId_PortfolioId",
                table: "Companies",
                columns: new[] { "PortfolioAppUserId", "PortfolioId" },
                principalTable: "Portfolios",
                principalColumns: new[] { "AppUserId", "Id" });
        }
    }
}
