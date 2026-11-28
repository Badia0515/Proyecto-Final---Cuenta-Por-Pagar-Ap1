using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CuentaPorPagar.Data.Migrations;

/// <inheritdoc />
public partial class _20261128001405_Actualizacion : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Estado",
            table: "devoluciones");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Estado",
            table: "devoluciones",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
    }
}
