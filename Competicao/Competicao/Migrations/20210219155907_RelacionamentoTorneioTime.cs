using Microsoft.EntityFrameworkCore.Migrations;

namespace Competicao.Migrations
{
    public partial class RelacionamentoTorneioTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TOR_ID",
                table: "TI_TIMES",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TI_TIMES_TOR_ID",
                table: "TI_TIMES",
                column: "TOR_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TI_TIMES_TOR_TORNEIOS_TOR_ID",
                table: "TI_TIMES",
                column: "TOR_ID",
                principalTable: "TOR_TORNEIOS",
                principalColumn: "TOR_ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TI_TIMES_TOR_TORNEIOS_TOR_ID",
                table: "TI_TIMES");

            migrationBuilder.DropIndex(
                name: "IX_TI_TIMES_TOR_ID",
                table: "TI_TIMES");

            migrationBuilder.DropColumn(
                name: "TOR_ID",
                table: "TI_TIMES");
        }
    }
}
