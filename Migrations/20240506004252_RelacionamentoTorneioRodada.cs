using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Codefast.Migrations
{
    /// <inheritdoc />
    public partial class RelacionamentoTorneioRodada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rodadas_Torneios_TorneioId",
                table: "Rodadas");

            migrationBuilder.DropColumn(
                name: "idTorneio",
                table: "Rodadas");

            migrationBuilder.AlterColumn<int>(
                name: "TorneioId",
                table: "Rodadas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rodadas_Torneios_TorneioId",
                table: "Rodadas",
                column: "TorneioId",
                principalTable: "Torneios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rodadas_Torneios_TorneioId",
                table: "Rodadas");

            migrationBuilder.AlterColumn<int>(
                name: "TorneioId",
                table: "Rodadas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "idTorneio",
                table: "Rodadas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Rodadas_Torneios_TorneioId",
                table: "Rodadas",
                column: "TorneioId",
                principalTable: "Torneios",
                principalColumn: "Id");
        }
    }
}
