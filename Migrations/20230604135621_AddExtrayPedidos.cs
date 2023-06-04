using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SullivanBurger.Migrations
{
    /// <inheritdoc />
    public partial class AddExtrayPedidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Extras",
                columns: table => new
                {
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extras", x => x.Nombre);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Usuarios_Email",
                        column: x => x.Email,
                        principalTable: "Usuarios",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductosPedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPedido = table.Column<int>(type: "int", nullable: false),
                    NombreProducto = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosPedidos", x => new { x.Id, x.IdPedido, x.NombreProducto });
                    table.ForeignKey(
                        name: "FK_ProductosPedidos_Pedidos_IdPedido",
                        column: x => x.IdPedido,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductosPedidos_Productos_NombreProducto",
                        column: x => x.NombreProducto,
                        principalTable: "Productos",
                        principalColumn: "Nombre",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_Email",
                table: "Pedidos",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosPedidos_IdPedido",
                table: "ProductosPedidos",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosPedidos_NombreProducto",
                table: "ProductosPedidos",
                column: "NombreProducto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Extras");

            migrationBuilder.DropTable(
                name: "ProductosPedidos");

            migrationBuilder.DropTable(
                name: "Pedidos");
        }
    }
}
