using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Codefast.Migrations
{
    /// <inheritdoc />
    public partial class EntidadeControleMataMata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDesclassificado",
                table: "ControleEliminatorias");

            migrationBuilder.AddColumn<bool>(
                name: "IsDesclassificado",
                table: "Equipes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDesclassificado",
                table: "Equipes");

            migrationBuilder.AddColumn<bool>(
                name: "IsDesclassificado",
                table: "ControleEliminatorias",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
