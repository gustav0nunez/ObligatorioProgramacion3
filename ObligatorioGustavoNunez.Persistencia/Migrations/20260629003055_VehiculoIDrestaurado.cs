using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObligatorioGustavoNunez.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class VehiculoIDrestaurado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Vehiculos_VehiculoId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_VehiculoId",
                table: "Reservas");

            migrationBuilder.AlterColumn<int>(
                name: "VehiculoMatricula",
                table: "Reservas",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VehiculoId",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Vehiculos_VehiculoMatricula",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_VehiculoMatricula",
                table: "Reservas");

            migrationBuilder.AlterColumn<string>(
                name: "VehiculoMatricula",
                table: "Reservas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VehiculoId",
                table: "Reservas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_VehiculoId",
                table: "Reservas",
                column: "VehiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Vehiculos_VehiculoId",
                table: "Reservas",
                column: "VehiculoId",
                principalTable: "Vehiculos",
                principalColumn: "Id");
        }
    }
}
