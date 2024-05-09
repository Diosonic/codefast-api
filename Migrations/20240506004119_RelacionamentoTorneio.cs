using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Codefast.Migrations
{
    /// <inheritdoc />
    public partial class RelacionamentoTorneio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TorneioId",
                table: "Rodadas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idTorneio",
                table: "Rodadas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rodadas_TorneioId",
                table: "Rodadas",
                column: "TorneioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rodadas_Torneios_TorneioId",
                table: "Rodadas",
                column: "TorneioId",
                principalTable: "Torneios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rodadas_Torneios_TorneioId",
                table: "Rodadas");

            migrationBuilder.DropIndex(
                name: "IX_Rodadas_TorneioId",
                table: "Rodadas");

            migrationBuilder.DropColumn(
                name: "TorneioId",
                table: "Rodadas");

            migrationBuilder.DropColumn(
                name: "idTorneio",
                table: "Rodadas");
        }
    }
}
