using Microsoft.EntityFrameworkCore.Migrations;

namespace Competicao.Migrations
{
    public partial class TipoTorneio_Torneio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoTorneio",
                table: "TOR_TORNEIOS",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoTorneio",
                table: "TOR_TORNEIOS");
        }
    }
}
