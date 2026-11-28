using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CuentaPorPagar.Data.Migrations;

/// <inheritdoc />
public partial class _20261127223326_Actualizado : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateOnly>(
            name: "FechaVencimiento",
            table: "Facturas",
            type: "date",
            nullable: false,
            defaultValue: new DateOnly(1, 1, 1));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "FechaVencimiento",
            table: "Facturas");
    }
}
