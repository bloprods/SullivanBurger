using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SullivanBurger.Migrations
{
    /// <inheritdoc />
    public partial class IDCambiados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Distribuidores");

            migrationBuilder.AlterColumn<string>(
                name: "DistribuidorId",
                table: "Productos",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Productos",
                table: "Productos",
                column: "Nombre");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Distribuidores",
                table: "Distribuidores",
                column: "Nombre");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Distribuidores_DistribuidorId",
                table: "Productos",
                column: "DistribuidorId",
                principalTable: "Distribuidores",
                principalColumn: "Nombre",
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

            migrationBuilder.AlterColumn<int>(
                name: "DistribuidorId",
                table: "Productos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Distribuidores",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

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
    }
}
