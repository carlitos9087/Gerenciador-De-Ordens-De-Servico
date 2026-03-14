using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoOscAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAcaoTomada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcaoTomada",
                table: "Oscs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcaoTomada",
                table: "Oscs");
        }
    }
}
