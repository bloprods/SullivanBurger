using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SullivanBurger.Migrations
{
    /// <inheritdoc />
    public partial class AddExtrayPedidos3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Usuarios_Email",
                table: "Pedidos");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Pedidos",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Usuarios_Email",
                table: "Pedidos",
                column: "Email",
                principalTable: "Usuarios",
                principalColumn: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Usuarios_Email",
                table: "Pedidos");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Pedidos",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Usuarios_Email",
                table: "Pedidos",
                column: "Email",
                principalTable: "Usuarios",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
