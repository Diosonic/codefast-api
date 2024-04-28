using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Codefast.Migrations
{
    /// <inheritdoc />
    public partial class ControleEliminatoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDesclassificado",
                table: "Equipes");

            migrationBuilder.DropColumn(
                name: "Pontuacao",
                table: "Equipes");

            migrationBuilder.DropColumn(
                name: "StatusValidacao",
                table: "Equipes");

            migrationBuilder.CreateTable(
                name: "ControleEliminatorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusValidacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDesclassificado = table.Column<bool>(type: "bit", nullable: false),
                    Pontuacao = table.Column<int>(type: "int", nullable: false),
                    EquipeId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_ControleEliminatorias_EquipeId",
                table: "ControleEliminatorias",
                column: "EquipeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControleEliminatorias");

            migrationBuilder.AddColumn<bool>(
                name: "IsDesclassificado",
                table: "Equipes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Pontuacao",
                table: "Equipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StatusValidacao",
                table: "Equipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
