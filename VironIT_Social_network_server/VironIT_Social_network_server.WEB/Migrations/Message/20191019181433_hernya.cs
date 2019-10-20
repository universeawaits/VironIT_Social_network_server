using Microsoft.EntityFrameworkCore.Migrations;

namespace VironIT_Social_network_server.WEB.Migrations.Message
{
    public partial class hernya : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "MessageMediaId",
                table: "Messages",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageMediaId",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "MediaId",
                table: "Messages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Messages",
                type: "text",
                nullable: true);
        }
    }
}
