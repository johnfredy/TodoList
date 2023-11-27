using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Migrations
{
    public partial class UserLogin3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "UserLogin",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "correo",
                table: "UserLogin",
                newName: "Correo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "UserLogin",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Correo",
                table: "UserLogin",
                newName: "correo");
        }
    }
}
