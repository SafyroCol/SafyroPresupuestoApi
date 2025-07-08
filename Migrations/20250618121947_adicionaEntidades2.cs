using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SafyroPresupuestos.Migrations
{
    /// <inheritdoc />
    public partial class adicionaEntidades2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dominio = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquiposDepreciacion_Empresas_EmpresaId",
                table: "EquiposDepreciacion");

            migrationBuilder.DropForeignKey(
                name: "FK_ManosObra_Empresas_EmpresaId",
                table: "ManosObra");

            migrationBuilder.DropForeignKey(
                name: "FK_Materiales_Empresas_EmpresaId",
                table: "Materiales");

            migrationBuilder.DropForeignKey(
                name: "FK_Proyectos_Empresas_EmpresaId",
                table: "Proyectos");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Empresas_EmpresaId1",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_EmpresaId1",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Proyectos_EmpresaId",
                table: "Proyectos");

            migrationBuilder.DropIndex(
                name: "IX_Presupuestos_ProyectoId",
                table: "Presupuestos");

            migrationBuilder.DropIndex(
                name: "IX_Materiales_EmpresaId",
                table: "Materiales");

            migrationBuilder.DropIndex(
                name: "IX_ManosObra_EmpresaId",
                table: "ManosObra");

            migrationBuilder.DropIndex(
                name: "IX_EquiposDepreciacion_EmpresaId",
                table: "EquiposDepreciacion");

            migrationBuilder.DropColumn(
                name: "EmpresaId1",
                table: "Usuarios");

            migrationBuilder.AddColumn<Guid>(
                name: "EmpresaId1",
                table: "Proyectos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EmpresaId1",
                table: "Materiales",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EmpresaId1",
                table: "ManosObra",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EmpresaId1",
                table: "EquiposDepreciacion",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Empresas",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EmpresaId",
                table: "Usuarios",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_EmpresaId1",
                table: "Proyectos",
                column: "EmpresaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuestos_ProyectoId",
                table: "Presupuestos",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Materiales_EmpresaId1",
                table: "Materiales",
                column: "EmpresaId1");

            migrationBuilder.CreateIndex(
                name: "IX_ManosObra_EmpresaId1",
                table: "ManosObra",
                column: "EmpresaId1");


            migrationBuilder.AddForeignKey(
                name: "FK_ManosObra_Empresas_EmpresaId1",
                table: "ManosObra",
                column: "EmpresaId1",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materiales_Empresas_EmpresaId1",
                table: "Materiales",
                column: "EmpresaId1",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proyectos_Empresas_EmpresaId1",
                table: "Proyectos",
                column: "EmpresaId1",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);


        }
    }
}
