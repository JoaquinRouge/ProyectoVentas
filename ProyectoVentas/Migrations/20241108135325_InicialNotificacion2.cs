using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoVentas.Migrations
{
    /// <inheritdoc />
    public partial class InicialNotificacion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificacionesStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrendaId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockActual = table.Column<int>(type: "int", nullable: false),
                    StockMinimo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificacionesStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificacionesStock_Prendas_PrendaId",
                        column: x => x.PrendaId,
                        principalTable: "Prendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificacionesStock_PrendaId",
                table: "NotificacionesStock",
                column: "PrendaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificacionesStock");
        }
    }
}
