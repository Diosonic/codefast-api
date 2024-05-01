using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Codefast.Migrations
{
    /// <inheritdoc />
    public partial class AlterarValorTempoParaTimeSpan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Tempo",
                table: "ControleEliminatorias",
                type: "time",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tempo",
                table: "ControleEliminatorias",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }
    }
}
