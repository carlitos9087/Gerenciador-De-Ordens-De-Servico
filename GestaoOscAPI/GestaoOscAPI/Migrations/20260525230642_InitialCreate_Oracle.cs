using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoOscAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_Oracle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Oscs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Equipamento = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    AcaoTomada = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    EmitenteId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    EmitenteNome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EmitenteSetor = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    QualidadeAssinou = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    EngenhariaAssinou = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    ProducaoAssinou = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oscs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Perfil = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Setor = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Oscs");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
