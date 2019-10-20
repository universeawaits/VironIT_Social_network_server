using Microsoft.EntityFrameworkCore.Migrations;

namespace VironIT_Social_network_server.WEB.Migrations.Message
{
    public partial class FK_null_fluent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessagesMedia_MessageMediaId",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessagesMedia_MessageMediaId",
                table: "Messages",
                column: "MessageMediaId",
                principalTable: "MessagesMedia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessagesMedia_MessageMediaId",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessagesMedia_MessageMediaId",
                table: "Messages",
                column: "MessageMediaId",
                principalTable: "MessagesMedia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
