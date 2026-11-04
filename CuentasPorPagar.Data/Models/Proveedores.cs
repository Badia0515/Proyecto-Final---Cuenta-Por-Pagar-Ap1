using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuentasPorPagar.Data.Models;

public class Proveedores
{
    [Key]
    public int ProveedorId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Rnc { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    [ForeignKey("EstadoB")]
    public bool Estado { get; set; }
}
