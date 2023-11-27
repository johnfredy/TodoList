using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Migrations
{
    public partial class userTarea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tarea");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tarea",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
