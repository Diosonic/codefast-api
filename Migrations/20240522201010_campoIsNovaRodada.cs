using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Codefast.Migrations
{
    /// <inheritdoc />
    public partial class campoIsNovaRodada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isNovaRodada",
                table: "Torneios",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isNovaRodada",
                table: "Torneios");
        }
    }
}
