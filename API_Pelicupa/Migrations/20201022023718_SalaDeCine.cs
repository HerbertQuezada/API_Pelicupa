using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Pelicula.Migrations
{
    public partial class SalaDeCine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalaDeCine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaDeCine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeliculaSalaDeCines",
                columns: table => new
                {
                    PeliculaID = table.Column<int>(nullable: false),
                    SalaDeCineID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculaSalaDeCines", x => new { x.PeliculaID, x.SalaDeCineID });
                    table.ForeignKey(
                        name: "FK_PeliculaSalaDeCines_Peliculas_PeliculaID",
                        column: x => x.PeliculaID,
                        principalTable: "Peliculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculaSalaDeCines_SalaDeCine_SalaDeCineID",
                        column: x => x.SalaDeCineID,
                        principalTable: "SalaDeCine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeliculaSalaDeCines_SalaDeCineID",
                table: "PeliculaSalaDeCines",
                column: "SalaDeCineID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeliculaSalaDeCines");

            migrationBuilder.DropTable(
                name: "SalaDeCine");
        }
    }
}
