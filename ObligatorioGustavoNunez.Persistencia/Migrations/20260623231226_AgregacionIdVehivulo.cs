using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObligatorioGustavoNunez.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class AgregacionIdVehivulo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Vehiculos_VehiculoMatricula",
                table: "Reservas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehiculos",
                table: "Vehiculos");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_VehiculoMatricula",
                table: "Reservas");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Vehiculos",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "VehiculoMatricula",
                table: "Reservas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "VehiculoId",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehiculos",
                table: "Vehiculos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_Matricula",
                table: "Vehiculos",
                column: "Matricula",
                unique: true);

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehiculos",
                table: "Vehiculos");

            migrationBuilder.DropIndex(
                name: "IX_Vehiculos_Matricula",
                table: "Vehiculos");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_VehiculoId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "VehiculoId",
                table: "Reservas");

            migrationBuilder.AlterColumn<string>(
                name: "VehiculoMatricula",
                table: "Reservas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehiculos",
                table: "Vehiculos",
                column: "Matricula");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_VehiculoMatricula",
                table: "Reservas",
                column: "VehiculoMatricula");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Vehiculos_VehiculoMatricula",
                table: "Reservas",
                column: "VehiculoMatricula",
                principalTable: "Vehiculos",
                principalColumn: "Matricula",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
