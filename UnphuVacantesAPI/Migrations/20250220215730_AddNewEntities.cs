using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnphuVacantesAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Vacantes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacantes_EmpresaId",
                table: "Vacantes",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Postulaciones_PostulanteId",
                table: "Postulaciones",
                column: "PostulanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Postulaciones_VacanteId",
                table: "Postulaciones",
                column: "VacanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Postulaciones_Postulantes_PostulanteId",
                table: "Postulaciones",
                column: "PostulanteId",
                principalTable: "Postulantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Postulaciones_Vacantes_VacanteId",
                table: "Postulaciones",
                column: "VacanteId",
                principalTable: "Vacantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacantes_Empresas_EmpresaId",
                table: "Vacantes",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postulaciones_Postulantes_PostulanteId",
                table: "Postulaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Postulaciones_Vacantes_VacanteId",
                table: "Postulaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacantes_Empresas_EmpresaId",
                table: "Vacantes");

            migrationBuilder.DropIndex(
                name: "IX_Vacantes_EmpresaId",
                table: "Vacantes");

            migrationBuilder.DropIndex(
                name: "IX_Postulaciones_PostulanteId",
                table: "Postulaciones");

            migrationBuilder.DropIndex(
                name: "IX_Postulaciones_VacanteId",
                table: "Postulaciones");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Vacantes");
        }
    }
}
