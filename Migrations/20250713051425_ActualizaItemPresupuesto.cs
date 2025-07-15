using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SafyroPresupuestos.Migrations
{
    /// <inheritdoc />
    public partial class ActualizaItemPresupuesto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CostoReal",
                table: "ItemPresupuesto",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostoReal",
                table: "ItemPresupuesto");
        }
    }
}
