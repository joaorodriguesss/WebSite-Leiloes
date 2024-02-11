using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteLeiloes.Migrations
{
    public partial class Migracao7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licitacao_Leilao_LeilaoId",
                table: "Licitacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Licitacao_Utilizador_UtilizadorId",
                table: "Licitacao");

            migrationBuilder.AddForeignKey(
                name: "FK_Licitacao_Leilao_LeilaoId",
                table: "Licitacao",
                column: "LeilaoId",
                principalTable: "Leilao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Licitacao_Utilizador_UtilizadorId",
                table: "Licitacao",
                column: "UtilizadorId",
                principalTable: "Utilizador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licitacao_Leilao_LeilaoId",
                table: "Licitacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Licitacao_Utilizador_UtilizadorId",
                table: "Licitacao");

            migrationBuilder.AddForeignKey(
                name: "FK_Licitacao_Leilao_LeilaoId",
                table: "Licitacao",
                column: "LeilaoId",
                principalTable: "Leilao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Licitacao_Utilizador_UtilizadorId",
                table: "Licitacao",
                column: "UtilizadorId",
                principalTable: "Utilizador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
