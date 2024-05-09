using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Codefast.Migrations
{
    /// <inheritdoc />
    public partial class SementeRodada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SementeRodadaId",
                table: "Rodadas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SementeRodada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SementeRodada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SementeRodada_Equipes_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rodadas_SementeRodadaId",
                table: "Rodadas",
                column: "SementeRodadaId");

            migrationBuilder.CreateIndex(
                name: "IX_SementeRodada_EquipeId",
                table: "SementeRodada",
                column: "EquipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rodadas_SementeRodada_SementeRodadaId",
                table: "Rodadas",
                column: "SementeRodadaId",
                principalTable: "SementeRodada",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rodadas_SementeRodada_SementeRodadaId",
                table: "Rodadas");

            migrationBuilder.DropTable(
                name: "SementeRodada");

            migrationBuilder.DropIndex(
                name: "IX_Rodadas_SementeRodadaId",
                table: "Rodadas");

            migrationBuilder.DropColumn(
                name: "SementeRodadaId",
                table: "Rodadas");
        }
    }
}
