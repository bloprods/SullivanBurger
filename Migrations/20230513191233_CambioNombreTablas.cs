using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SullivanBurger.Migrations
{
    /// <inheritdoc />
    public partial class CambioNombreTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Distribuidor_DistribuidorId",
                table: "Producto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Producto",
                table: "Producto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Distribuidor",
                table: "Distribuidor");

            migrationBuilder.RenameTable(
                name: "Producto",
                newName: "Productos");

            migrationBuilder.RenameTable(
                name: "Distribuidor",
                newName: "Distribuidores");

            migrationBuilder.RenameIndex(
                name: "IX_Producto_DistribuidorId",
                table: "Productos",
                newName: "IX_Productos_DistribuidorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Productos",
                table: "Productos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Distribuidores",
                table: "Distribuidores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Distribuidores_DistribuidorId",
                table: "Productos",
                column: "DistribuidorId",
                principalTable: "Distribuidores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Distribuidores_DistribuidorId",
                table: "Productos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Productos",
                table: "Productos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Distribuidores",
                table: "Distribuidores");

            migrationBuilder.RenameTable(
                name: "Productos",
                newName: "Producto");

            migrationBuilder.RenameTable(
                name: "Distribuidores",
                newName: "Distribuidor");

            migrationBuilder.RenameIndex(
                name: "IX_Productos_DistribuidorId",
                table: "Producto",
                newName: "IX_Producto_DistribuidorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Producto",
                table: "Producto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Distribuidor",
                table: "Distribuidor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Distribuidor_DistribuidorId",
                table: "Producto",
                column: "DistribuidorId",
                principalTable: "Distribuidor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
