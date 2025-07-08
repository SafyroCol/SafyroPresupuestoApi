using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SafyroPresupuestos.Migrations
{
    /// <inheritdoc />
    public partial class adicionaEntidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Empresas_EmpresaId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PERMISO_USUARIO_AspNetUsers_UsuarioId1",
                table: "PERMISO_USUARIO");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmpresaId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
            name: "MaterialPresupuesto",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),

                PresupuestoId = table.Column<int>(type: "int", nullable: false),
                MaterialId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MaterialPresupuesto", x => x.Id);
                table.ForeignKey(
                    name: "FK_MaterialPresupuesto_Materiales_MaterialId",
                    column: x => x.MaterialId,
                    principalTable: "Materiales",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_MaterialPresupuesto_Presupuestos_PresupuestoId",
                    column: x => x.PresupuestoId,
                    principalTable: "Presupuestos",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });




            migrationBuilder.AddColumn<decimal>(
                name: "Cantidad",
                table: "MaterialPresupuesto",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CostoUnitario",
                table: "MaterialPresupuesto",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UnidadDeMedida",
                table: "MaterialPresupuesto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                       name: "EquipoPresupuesto",
                       columns: table => new
                       {
                           Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                           PresupuestoId = table.Column<int>(type: "int", nullable: false),
                           EquipoDepreciacionId = table.Column<int>(type: "int", nullable: false)
                       },
                       constraints: table =>
                       {
                           table.PrimaryKey("PK_EquipoPresupuesto", x => x.Id);
                           table.ForeignKey(
                               name: "FK_EquipoPresupuesto_EquiposDepreciacion_EquipoDepreciacionId",
                               column: x => x.EquipoDepreciacionId,
                               principalTable: "EquiposDepreciacion",
                               principalColumn: "Id",
                               onDelete: ReferentialAction.Restrict);
                           table.ForeignKey(
                               name: "FK_EquipoPresupuesto_Presupuestos_PresupuestoId",
                               column: x => x.PresupuestoId,
                               principalTable: "Presupuestos",
                               principalColumn: "Id",
                               onDelete: ReferentialAction.Restrict);
                       });
         

            migrationBuilder.CreateTable(
                name: "EquipoRealPresupuesto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PresupuestoId = table.Column<int>(type: "int", nullable: false),
                    EquipoPresupuestoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostoReal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipoRealPresupuesto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipoRealPresupuesto_EquipoPresupuesto_EquipoPresupuestoId",
                        column: x => x.EquipoPresupuestoId,
                        principalTable: "EquipoPresupuesto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipoRealPresupuesto_Presupuestos_PresupuestoId",
                        column: x => x.PresupuestoId,
                        principalTable: "Presupuestos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialRealPresupuesto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PresupuestoId = table.Column<int>(type: "int", nullable: false),
                    MaterialPresupuestoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostoReal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialRealPresupuesto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipoRealPresupuesto_EquipoPresupuestoId",
                table: "EquipoRealPresupuesto",
                column: "EquipoPresupuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoRealPresupuesto_PresupuestoId",
                table: "EquipoRealPresupuesto",
                column: "PresupuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EmpresaId",
                table: "Usuarios",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PERMISO_USUARIO_Usuarios_UsuarioId1",
                table: "PERMISO_USUARIO",
                column: "UsuarioId1",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PERMISO_USUARIO_Usuarios_UsuarioId1",
                table: "PERMISO_USUARIO");

            migrationBuilder.DropTable(
                name: "EquipoRealPresupuesto");

            migrationBuilder.DropTable(
                name: "MaterialRealPresupuesto");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "MaterialPresupuesto");

            migrationBuilder.DropColumn(
                name: "CostoUnitario",
                table: "MaterialPresupuesto");

            migrationBuilder.DropColumn(
                name: "UnidadDeMedida",
                table: "MaterialPresupuesto");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MaterialPresupuesto",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "EquipoPresupuesto",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmpresaId",
                table: "AspNetUsers",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Empresas_EmpresaId",
                table: "AspNetUsers",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PERMISO_USUARIO_AspNetUsers_UsuarioId1",
                table: "PERMISO_USUARIO",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
