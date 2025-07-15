using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SafyroPresupuestos.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaCostosRealesAPresupuesto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CostoRealCostosIndirectos",
                table: "Presupuestos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CostoRealEquipos",
                table: "Presupuestos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CostoRealManoObra",
                table: "Presupuestos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CostoRealMateriales",
                table: "Presupuestos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CostoRealServiciosTerceros",
                table: "Presupuestos",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostoRealCostosIndirectos",
                table: "Presupuestos");

            migrationBuilder.DropColumn(
                name: "CostoRealEquipos",
                table: "Presupuestos");

            migrationBuilder.DropColumn(
                name: "CostoRealManoObra",
                table: "Presupuestos");

            migrationBuilder.DropColumn(
                name: "CostoRealMateriales",
                table: "Presupuestos");

            migrationBuilder.DropColumn(
                name: "CostoRealServiciosTerceros",
                table: "Presupuestos");
        }
    }
}
