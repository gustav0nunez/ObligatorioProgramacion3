using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObligatorioGustavoNunez.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTablaUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Vehiculos_VehiculoMatricula",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_VehiculoMatricula",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "VehiculoMatricula",
                table: "Reservas");

            migrationBuilder.RenameColumn(
                name: "Contraseña",
                table: "Usuarios",
                newName: "Contrasena");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_VehiculoId",
                table: "Reservas",
                column: "VehiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Vehiculos_VehiculoId",
                table: "Reservas",
                column: "VehiculoId",
                principalTable: "Vehiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Vehiculos_VehiculoId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_VehiculoId",
                table: "Reservas");

            migrationBuilder.RenameColumn(
                name: "Contrasena",
                table: "Usuarios",
                newName: "Contraseña");

            migrationBuilder.AddColumn<int>(
                name: "VehiculoMatricula",
                table: "Reservas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_VehiculoMatricula",
                table: "Reservas",
                column: "VehiculoMatricula");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Vehiculos_VehiculoMatricula",
                table: "Reservas",
                column: "VehiculoMatricula",
                principalTable: "Vehiculos",
                principalColumn: "Id");
        }
    }
}
