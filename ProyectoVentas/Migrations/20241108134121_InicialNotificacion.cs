using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoVentas.Migrations
{
    /// <inheritdoc />
    public partial class InicialNotificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockMinimo",
                table: "Prendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProd = table.Column<int>(type: "int", nullable: false),
                    cant = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropColumn(
                name: "StockMinimo",
                table: "Prendas");
        }
    }
}
