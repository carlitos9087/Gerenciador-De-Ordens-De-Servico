using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoOscAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFlags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oscs_Usuarios_GerenteEngenhariaId",
                table: "Oscs");

            migrationBuilder.DropForeignKey(
                name: "FK_Oscs_Usuarios_GerenteProducaoId",
                table: "Oscs");

            migrationBuilder.DropForeignKey(
                name: "FK_Oscs_Usuarios_GerenteQualidadeId",
                table: "Oscs");

            migrationBuilder.DropIndex(
                name: "IX_Oscs_GerenteEngenhariaId",
                table: "Oscs");

            migrationBuilder.DropIndex(
                name: "IX_Oscs_GerenteProducaoId",
                table: "Oscs");

            migrationBuilder.DropIndex(
                name: "IX_Oscs_GerenteQualidadeId",
                table: "Oscs");

            migrationBuilder.DropColumn(
                name: "GerenteEngenhariaId",
                table: "Oscs");

            migrationBuilder.DropColumn(
                name: "GerenteProducaoId",
                table: "Oscs");

            migrationBuilder.DropColumn(
                name: "GerenteQualidadeId",
                table: "Oscs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GerenteEngenhariaId",
                table: "Oscs",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GerenteProducaoId",
                table: "Oscs",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GerenteQualidadeId",
                table: "Oscs",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Oscs_GerenteEngenhariaId",
                table: "Oscs",
                column: "GerenteEngenhariaId");

            migrationBuilder.CreateIndex(
                name: "IX_Oscs_GerenteProducaoId",
                table: "Oscs",
                column: "GerenteProducaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Oscs_GerenteQualidadeId",
                table: "Oscs",
                column: "GerenteQualidadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Oscs_Usuarios_GerenteEngenhariaId",
                table: "Oscs",
                column: "GerenteEngenhariaId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Oscs_Usuarios_GerenteProducaoId",
                table: "Oscs",
                column: "GerenteProducaoId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Oscs_Usuarios_GerenteQualidadeId",
                table: "Oscs",
                column: "GerenteQualidadeId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
