using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace API_Pelicula.Migrations
{
    public partial class SalaCineUbicacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Point>(
                name: "Ubicacion",
                table: "SalaDeCine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ubicacion",
                table: "SalaDeCine");
        }
    }
}
