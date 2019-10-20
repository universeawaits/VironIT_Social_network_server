using Microsoft.EntityFrameworkCore.Migrations;

namespace VironIT_Social_network_server.WEB.Migrations.Message
{
    public partial class FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageMediaId",
                table: "Messages",
                column: "MessageMediaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessagesMedia_MessageMediaId",
                table: "Messages",
                column: "MessageMediaId",
                principalTable: "MessagesMedia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessagesMedia_MessageMediaId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_MessageMediaId",
                table: "Messages");
        }
    }
}
