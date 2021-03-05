using Microsoft.EntityFrameworkCore.Migrations;

namespace Competicao.Migrations
{
    public partial class TorneioUsuarioRelacionamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioID",
                table: "TOR_TORNEIOS",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TOR_TORNEIOS_UsuarioID",
                table: "TOR_TORNEIOS",
                column: "UsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_TOR_TORNEIOS_AspNetUsers_UsuarioID",
                table: "TOR_TORNEIOS",
                column: "UsuarioID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TOR_TORNEIOS_AspNetUsers_UsuarioID",
                table: "TOR_TORNEIOS");

            migrationBuilder.DropIndex(
                name: "IX_TOR_TORNEIOS_UsuarioID",
                table: "TOR_TORNEIOS");

            migrationBuilder.DropColumn(
                name: "UsuarioID",
                table: "TOR_TORNEIOS");
        }
    }
}
