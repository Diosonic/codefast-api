using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Codefast.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoInicialSQlite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Torneios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Torneios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    IsCredenciado = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDesclassificado = table.Column<bool>(type: "INTEGER", nullable: false),
                    NomeParticipantes = table.Column<string>(type: "TEXT", nullable: false),
                    TorneioId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipes_Torneios_TorneioId",
                        column: x => x.TorneioId,
                        principalTable: "Torneios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rodadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    TorneioId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rodadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rodadas_Torneios_TorneioId",
                        column: x => x.TorneioId,
                        principalTable: "Torneios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ControleEliminatorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StatusValidacao = table.Column<string>(type: "TEXT", nullable: false),
                    Pontuacao = table.Column<int>(type: "INTEGER", nullable: false),
                    Tempo = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    EquipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDesclassificado = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControleEliminatorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControleEliminatorias_Equipes_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ControleMataMatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StatusValidacao = table.Column<string>(type: "TEXT", nullable: false),
                    EquipeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControleMataMatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControleMataMatas_Equipes_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SementeRodadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RodadaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SementeRodadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SementeRodadas_Rodadas_RodadaId",
                        column: x => x.RodadaId,
                        principalTable: "Rodadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipeSementeRodada",
                columns: table => new
                {
                    EquipesId = table.Column<int>(type: "INTEGER", nullable: false),
                    SementeRodadasId = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "IX_ControleEliminatorias_EquipeId",
                table: "ControleEliminatorias",
                column: "EquipeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ControleMataMatas_EquipeId",
                table: "ControleMataMatas",
                column: "EquipeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipes_TorneioId",
                table: "Equipes",
                column: "TorneioId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipeSementeRodada_SementeRodadasId",
                table: "EquipeSementeRodada",
                column: "SementeRodadasId");

            migrationBuilder.CreateIndex(
                name: "IX_Rodadas_TorneioId",
                table: "Rodadas",
                column: "TorneioId");

            migrationBuilder.CreateIndex(
                name: "IX_SementeRodadas_RodadaId",
                table: "SementeRodadas",
                column: "RodadaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControleEliminatorias");

            migrationBuilder.DropTable(
                name: "ControleMataMatas");

            migrationBuilder.DropTable(
                name: "EquipeSementeRodada");

            migrationBuilder.DropTable(
                name: "Equipes");

            migrationBuilder.DropTable(
                name: "SementeRodadas");

            migrationBuilder.DropTable(
                name: "Rodadas");

            migrationBuilder.DropTable(
                name: "Torneios");
        }
    }
}
