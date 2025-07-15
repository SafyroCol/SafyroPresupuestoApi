using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SafyroPresupuestos.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaEvidenciasItemPresupuesto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvidenciasItemPresupuesto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemPresupuestoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreArchivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlArchivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvidenciasItemPresupuesto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvidenciasItemPresupuesto_ItemPresupuesto_ItemPresupuestoId",
                        column: x => x.ItemPresupuestoId,
                        principalTable: "ItemPresupuesto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvidenciasItemPresupuesto_ItemPresupuestoId",
                table: "EvidenciasItemPresupuesto",
                column: "ItemPresupuestoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvidenciasItemPresupuesto");
        }
    }
}
