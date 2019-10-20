using Microsoft.EntityFrameworkCore.Migrations;

namespace VironIT_Social_network_server.WEB.Migrations.Image
{
    public partial class audios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Audio",
                table: "Audio");

            migrationBuilder.RenameTable(
                name: "Audio",
                newName: "Audios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Audios",
                table: "Audios",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Audios",
                table: "Audios");

            migrationBuilder.RenameTable(
                name: "Audios",
                newName: "Audio");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Audio",
                table: "Audio",
                column: "Id");
        }
    }
}
