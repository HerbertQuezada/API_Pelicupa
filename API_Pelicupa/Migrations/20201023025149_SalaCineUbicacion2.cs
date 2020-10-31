using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Pelicula.Migrations
{
    public partial class SalaCineUbicacion2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeliculaSalaDeCines_SalaDeCine_SalaDeCineID",
                table: "PeliculaSalaDeCines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalaDeCine",
                table: "SalaDeCine");

            migrationBuilder.RenameTable(
                name: "SalaDeCine",
                newName: "SalasDeCine");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalasDeCine",
                table: "SalasDeCine",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculaSalaDeCines_SalasDeCine_SalaDeCineID",
                table: "PeliculaSalaDeCines",
                column: "SalaDeCineID",
                principalTable: "SalasDeCine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeliculaSalaDeCines_SalasDeCine_SalaDeCineID",
                table: "PeliculaSalaDeCines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalasDeCine",
                table: "SalasDeCine");

            migrationBuilder.RenameTable(
                name: "SalasDeCine",
                newName: "SalaDeCine");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalaDeCine",
                table: "SalaDeCine",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculaSalaDeCines_SalaDeCine_SalaDeCineID",
                table: "PeliculaSalaDeCines",
                column: "SalaDeCineID",
                principalTable: "SalaDeCine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
