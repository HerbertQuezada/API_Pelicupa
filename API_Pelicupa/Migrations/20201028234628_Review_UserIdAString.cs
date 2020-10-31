using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Pelicula.Migrations
{
    public partial class Review_UserIdAString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UsuarioId1",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UsuarioId1",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Reviews",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UsuarioId",
                table: "Reviews",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UsuarioId",
                table: "Reviews",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UsuarioId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UsuarioId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UsuarioId1",
                table: "Reviews",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UsuarioId1",
                table: "Reviews",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
