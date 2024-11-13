using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GladLogsApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserandChatIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Chats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Chats",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
