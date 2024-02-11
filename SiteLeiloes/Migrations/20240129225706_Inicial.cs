using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteLeiloes.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Licitacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeilaoId = table.Column<int>(type: "int", nullable: false),
                    UtilizadorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licitacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Licitacao_Leilao_LeilaoId",
                        column: x => x.LeilaoId,
                        principalTable: "Leilao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Licitacao_Utilizador_UtilizadorId",
                        column: x => x.UtilizadorId,
                        principalTable: "Utilizador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Licitacao_LeilaoId",
                table: "Licitacao",
                column: "LeilaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Licitacao_UtilizadorId",
                table: "Licitacao",
                column: "UtilizadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Licitacao");
        }
    }
}
