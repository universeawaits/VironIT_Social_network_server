using Microsoft.EntityFrameworkCore.Migrations;

namespace VironIT_Social_network_server.WEB.Migrations.Image
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageSize",
                table: "Avatars");

            migrationBuilder.AddColumn<string>(
                name: "SizeCategory",
                table: "Avatars",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SizeCategory",
                table: "Avatars");

            migrationBuilder.AddColumn<string>(
                name: "ImageSize",
                table: "Avatars",
                type: "text",
                nullable: true);
        }
    }
}
