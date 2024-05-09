using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Codefast.Migrations
{
    /// <inheritdoc />
    public partial class ReestruturacaoRodada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rodadas_SementeRodada_SementeRodadaId",
                table: "Rodadas");

            migrationBuilder.DropForeignKey(
                name: "FK_SementeRodada_Equipes_EquipeId",
                table: "SementeRodada");

            migrationBuilder.DropIndex(
                name: "IX_Rodadas_SementeRodadaId",
                table: "Rodadas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SementeRodada",
                table: "SementeRodada");

            migrationBuilder.DropColumn(
                name: "SementeRodadaId",
                table: "Rodadas");

            migrationBuilder.DropColumn(
                name: "TorneioId",
                table: "Rodadas");

            migrationBuilder.RenameTable(
                name: "SementeRodada",
                newName: "SementeRodadas");

            migrationBuilder.RenameColumn(
                name: "EquipeId",
                table: "SementeRodadas",
                newName: "RodadaId");

            migrationBuilder.RenameIndex(
                name: "IX_SementeRodada_EquipeId",
                table: "SementeRodadas",
                newName: "IX_SementeRodadas_RodadaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SementeRodadas",
                table: "SementeRodadas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EquipeSementeRodada",
                columns: table => new
                {
                    EquipesId = table.Column<int>(type: "int", nullable: false),
                    SementeRodadasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipeSementeRodada", x => new { x.EquipesId, x.SementeRodadasId });
                    table.ForeignKey(
                        name: "FK_EquipeSementeRodada_Equipes_EquipesId",
                        column: x => x.EquipesId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipeSementeRodada_SementeRodadas_SementeRodadasId",
                        column: x => x.SementeRodadasId,
                        principalTable: "SementeRodadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipeSementeRodada_SementeRodadasId",
                table: "EquipeSementeRodada",
                column: "SementeRodadasId");

            migrationBuilder.AddForeignKey(
                name: "FK_SementeRodadas_Rodadas_RodadaId",
                table: "SementeRodadas",
                column: "RodadaId",
                principalTable: "Rodadas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SementeRodadas_Rodadas_RodadaId",
                table: "SementeRodadas");

            migrationBuilder.DropTable(
                name: "EquipeSementeRodada");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SementeRodadas",
                table: "SementeRodadas");

            migrationBuilder.RenameTable(
                name: "SementeRodadas",
                newName: "SementeRodada");

            migrationBuilder.RenameColumn(
                name: "RodadaId",
                table: "SementeRodada",
                newName: "EquipeId");

            migrationBuilder.RenameIndex(
                name: "IX_SementeRodadas_RodadaId",
                table: "SementeRodada",
                newName: "IX_SementeRodada_EquipeId");

            migrationBuilder.AddColumn<int>(
                name: "SementeRodadaId",
                table: "Rodadas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TorneioId",
                table: "Rodadas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SementeRodada",
                table: "SementeRodada",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rodadas_SementeRodadaId",
                table: "Rodadas",
                column: "SementeRodadaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rodadas_SementeRodada_SementeRodadaId",
                table: "Rodadas",
                column: "SementeRodadaId",
                principalTable: "SementeRodada",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SementeRodada_Equipes_EquipeId",
                table: "SementeRodada",
                column: "EquipeId",
                principalTable: "Equipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
