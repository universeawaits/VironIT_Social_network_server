using Microsoft.EntityFrameworkCore.Migrations;

namespace VironIT_Social_network_server.WEB.Migrations.Image
{
    public partial class media : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Avatars",
                table: "Avatars");

            migrationBuilder.RenameTable(
                name: "Avatars",
                newName: "Media");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Media",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Media",
                table: "Media",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Media",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Media");

            migrationBuilder.RenameTable(
                name: "Media",
                newName: "Avatars");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Avatars",
                table: "Avatars",
                column: "Id");
        }
    }
}
