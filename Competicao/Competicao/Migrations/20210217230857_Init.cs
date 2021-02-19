using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Competicao.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TI_TIMES",
                columns: table => new
                {
                    TI_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TI_NOME = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TI_TIMES", x => x.TI_ID);
                });

            migrationBuilder.CreateTable(
                name: "TOR_TORNEIOS",
                columns: table => new
                {
                    TOR_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TOR_NOME = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    TOR_CRIACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TOR_TORNEIOS", x => x.TOR_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TI_TIMES");

            migrationBuilder.DropTable(
                name: "TOR_TORNEIOS");
        }
    }
}
