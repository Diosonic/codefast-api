using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Codefast.Migrations
{
    /// <inheritdoc />
    public partial class RemocaoCampoEmControleEliminatoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDesclassificado",
                table: "ControleEliminatorias");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDesclassificado",
                table: "ControleEliminatorias",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
