using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Codefast.Migrations
{
    /// <inheritdoc />
    public partial class uniqueKeyparaControleEliminatorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ControleMataMatas_EquipeId",
                table: "ControleMataMatas");

            migrationBuilder.CreateTable(
                name: "Rodadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TorneioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rodadas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ControleMataMatas_EquipeId",
                table: "ControleMataMatas",
                column: "EquipeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rodadas");

            migrationBuilder.DropIndex(
                name: "IX_ControleMataMatas_EquipeId",
                table: "ControleMataMatas");

            migrationBuilder.CreateIndex(
                name: "IX_ControleMataMatas_EquipeId",
                table: "ControleMataMatas",
                column: "EquipeId");
        }
    }
}
