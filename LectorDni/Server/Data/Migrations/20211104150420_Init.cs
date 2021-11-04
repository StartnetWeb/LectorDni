using Microsoft.EntityFrameworkCore.Migrations;

namespace LectorDni.Server.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatosDni",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NroTramite = table.Column<string>(nullable: true),
                    Apellido = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Sexo = table.Column<string>(nullable: true),
                    Dni = table.Column<string>(nullable: true),
                    Ejemplar = table.Column<string>(nullable: true),
                    FechaNacimiento = table.Column<string>(nullable: true),
                    FechaEmision = table.Column<string>(nullable: true),
                    Dato = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosDni", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatosDni");
        }
    }
}
