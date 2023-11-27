using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Migrations
{
    public partial class usertareas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Tarea",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tarea_UsuarioId",
                table: "Tarea",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarea_UserLogin_UsuarioId",
                table: "Tarea",
                column: "UsuarioId",
                principalTable: "UserLogin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarea_UserLogin_UsuarioId",
                table: "Tarea");

            migrationBuilder.DropIndex(
                name: "IX_Tarea_UsuarioId",
                table: "Tarea");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Tarea");
        }
    }
}
