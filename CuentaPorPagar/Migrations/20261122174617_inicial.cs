using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CuentasPorPagar.Migrations;

/// <inheritdoc />
public partial class _20261122174617_inicial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Proveedores",
            columns: table => new
            {
                ProveedorId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Rnc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Balance = table.Column<double>(type: "float", nullable: false),
                Estado = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Proveedores", x => x.ProveedorId);
            });

        migrationBuilder.CreateTable(
            name: "Usuarios",
            columns: table => new
            {
                UsuarioId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
            });

        migrationBuilder.CreateTable(
            name: "Facturas",
            columns: table => new
            {
                FacturasId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                NumFactura = table.Column<int>(type: "int", nullable: false),
                ProveedorId = table.Column<int>(type: "int", nullable: false),
                Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Facturas", x => x.FacturasId);
                table.ForeignKey(
                    name: "FK_Facturas_Proveedores_ProveedorId",
                    column: x => x.ProveedorId,
                    principalTable: "Proveedores",
                    principalColumn: "ProveedorId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "devoluciones",
            columns: table => new
            {
                DevolucionId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                FacturasId = table.Column<int>(type: "int", nullable: false),
                Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                Monto = table.Column<double>(type: "float", nullable: false),
                Motivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_devoluciones", x => x.DevolucionId);
                table.ForeignKey(
                    name: "FK_devoluciones_Facturas_FacturasId",
                    column: x => x.FacturasId,
                    principalTable: "Facturas",
                    principalColumn: "FacturasId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "FacturaDetalle",
            columns: table => new
            {
                DetalleFacturaId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                FacturasId = table.Column<int>(type: "int", nullable: false),
                Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Cantidad = table.Column<int>(type: "int", nullable: false),
                ProveedorId = table.Column<int>(type: "int", nullable: false),
                Precio = table.Column<double>(type: "float", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FacturaDetalle", x => x.DetalleFacturaId);
                table.ForeignKey(
                    name: "FK_FacturaDetalle_Facturas_FacturasId",
                    column: x => x.FacturasId,
                    principalTable: "Facturas",
                    principalColumn: "FacturasId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "pagos",
            columns: table => new
            {
                PagosId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                FacturaId = table.Column<int>(type: "int", nullable: false),
                FechaProgramada = table.Column<DateOnly>(type: "date", nullable: false),
                Monto = table.Column<double>(type: "float", nullable: false),
                Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Referencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                TipoPago = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_pagos", x => x.PagosId);
                table.ForeignKey(
                    name: "FK_pagos_Facturas_FacturaId",
                    column: x => x.FacturaId,
                    principalTable: "Facturas",
                    principalColumn: "FacturasId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Aprovaciones",
            columns: table => new
            {
                AprovacionId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PagoId = table.Column<int>(type: "int", nullable: false),
                FechaAprobacion = table.Column<DateOnly>(type: "date", nullable: false),
                UsuarioId = table.Column<int>(type: "int", nullable: false),
                Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Comentario = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Aprovaciones", x => x.AprovacionId);
                table.ForeignKey(
                    name: "FK_Aprovaciones_Usuarios_UsuarioId",
                    column: x => x.UsuarioId,
                    principalTable: "Usuarios",
                    principalColumn: "UsuarioId",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Aprovaciones_pagos_PagoId",
                    column: x => x.PagoId,
                    principalTable: "pagos",
                    principalColumn: "PagosId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ProveedorDetalle",
            columns: table => new
            {
                DetalleId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ProveedorId = table.Column<int>(type: "int", nullable: false),
                PagoId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProveedorDetalle", x => x.DetalleId);
                table.ForeignKey(
                    name: "FK_ProveedorDetalle_Proveedores_ProveedorId",
                    column: x => x.ProveedorId,
                    principalTable: "Proveedores",
                    principalColumn: "ProveedorId",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ProveedorDetalle_pagos_PagoId",
                    column: x => x.PagoId,
                    principalTable: "pagos",
                    principalColumn: "PagosId");
            });

        migrationBuilder.CreateTable(
            name: "transacciones",
            columns: table => new
            {
                TransaccionId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PagoId = table.Column<int>(type: "int", nullable: false),
                UsuarioId = table.Column<int>(type: "int", nullable: false),
                Accion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                Detalle = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_transacciones", x => x.TransaccionId);
                table.ForeignKey(
                    name: "FK_transacciones_Usuarios_UsuarioId",
                    column: x => x.UsuarioId,
                    principalTable: "Usuarios",
                    principalColumn: "UsuarioId",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_transacciones_pagos_PagoId",
                    column: x => x.PagoId,
                    principalTable: "pagos",
                    principalColumn: "PagosId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Aprovaciones_PagoId",
            table: "Aprovaciones",
            column: "PagoId");

        migrationBuilder.CreateIndex(
            name: "IX_Aprovaciones_UsuarioId",
            table: "Aprovaciones",
            column: "UsuarioId");

        migrationBuilder.CreateIndex(
            name: "IX_devoluciones_FacturasId",
            table: "devoluciones",
            column: "FacturasId");

        migrationBuilder.CreateIndex(
            name: "IX_FacturaDetalle_FacturasId",
            table: "FacturaDetalle",
            column: "FacturasId");

        migrationBuilder.CreateIndex(
            name: "IX_Facturas_ProveedorId",
            table: "Facturas",
            column: "ProveedorId");

        migrationBuilder.CreateIndex(
            name: "IX_pagos_FacturaId",
            table: "pagos",
            column: "FacturaId");

        migrationBuilder.CreateIndex(
            name: "IX_ProveedorDetalle_PagoId",
            table: "ProveedorDetalle",
            column: "PagoId");

        migrationBuilder.CreateIndex(
            name: "IX_ProveedorDetalle_ProveedorId",
            table: "ProveedorDetalle",
            column: "ProveedorId");

        migrationBuilder.CreateIndex(
            name: "IX_transacciones_PagoId",
            table: "transacciones",
            column: "PagoId");

        migrationBuilder.CreateIndex(
            name: "IX_transacciones_UsuarioId",
            table: "transacciones",
            column: "UsuarioId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Aprovaciones");

        migrationBuilder.DropTable(
            name: "devoluciones");

        migrationBuilder.DropTable(
            name: "FacturaDetalle");

        migrationBuilder.DropTable(
            name: "ProveedorDetalle");

        migrationBuilder.DropTable(
            name: "transacciones");

        migrationBuilder.DropTable(
            name: "Usuarios");

        migrationBuilder.DropTable(
            name: "pagos");

        migrationBuilder.DropTable(
            name: "Facturas");

        migrationBuilder.DropTable(
            name: "Proveedores");
    }
}
