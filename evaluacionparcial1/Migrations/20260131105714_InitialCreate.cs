using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evaluacionparcial1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Albumes",
                columns: table => new
                {
                    album_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    año_lanzamiento = table.Column<int>(type: "int", nullable: false),
                    Discografica = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albumes", x => x.album_id);
                });

            migrationBuilder.CreateTable(
                name: "Artistas",
                columns: table => new
                {
                    artista_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fecha_nacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nacionalidad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artistas", x => x.artista_id);
                });

            migrationBuilder.CreateTable(
                name: "AlbumArtistas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    album_id = table.Column<int>(type: "int", nullable: false),
                    artista_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumArtistas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlbumArtistas_Albumes_album_id",
                        column: x => x.album_id,
                        principalTable: "Albumes",
                        principalColumn: "album_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumArtistas_Artistas_artista_id",
                        column: x => x.artista_id,
                        principalTable: "Artistas",
                        principalColumn: "artista_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumArtistas_album_id_artista_id",
                table: "AlbumArtistas",
                columns: new[] { "album_id", "artista_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AlbumArtistas_artista_id",
                table: "AlbumArtistas",
                column: "artista_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumArtistas");

            migrationBuilder.DropTable(
                name: "Albumes");

            migrationBuilder.DropTable(
                name: "Artistas");
        }
    }
}
