using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Migrations
{
    public partial class userTarea3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarea_UserLogin_UsuarioId",
                table: "Tarea");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Tarea",
                newName: "UserLoginId");

            migrationBuilder.RenameIndex(
                name: "IX_Tarea_UsuarioId",
                table: "Tarea",
                newName: "IX_Tarea_UserLoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarea_UserLogin_UserLoginId",
                table: "Tarea",
                column: "UserLoginId",
                principalTable: "UserLogin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarea_UserLogin_UserLoginId",
                table: "Tarea");

            migrationBuilder.RenameColumn(
                name: "UserLoginId",
                table: "Tarea",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Tarea_UserLoginId",
                table: "Tarea",
                newName: "IX_Tarea_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarea_UserLogin_UsuarioId",
                table: "Tarea",
                column: "UsuarioId",
                principalTable: "UserLogin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
