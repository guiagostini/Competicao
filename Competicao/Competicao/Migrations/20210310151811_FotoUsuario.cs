using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Competicao.Migrations
{
    public partial class FotoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FotoMimeType",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FotoMimeType",
                table: "AspNetUsers");
        }
    }
}
