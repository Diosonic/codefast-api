using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Codefast.Migrations
{
    /// <inheritdoc />
    public partial class ControleEliminatoriaAddTempo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tempo",
                table: "ControleEliminatorias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tempo",
                table: "ControleEliminatorias");
        }
    }
}
