using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoVentas.Migrations
{
    /// <inheritdoc />
    public partial class InicialRepo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cant",
                table: "Reposiciones",
                newName: "Cantidad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cantidad",
                table: "Reposiciones",
                newName: "cant");
        }
    }
}
