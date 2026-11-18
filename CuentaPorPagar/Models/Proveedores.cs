using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuentasPorPagar.Data.Models;

public class Proveedores
{
    [Key]
    public int ProveedorId { get; set; }

    [Required(ErrorMessage ="Debe ingresar el nombre del proveedor")]
    public string Nombre { get; set; } = null!; 

    [Required(ErrorMessage ="Debe ingresar el Rnc del proveedor")]
    public string Rnc { get; set; } = null!;

    [Required(ErrorMessage ="Debe ingrese el numero de Telefono del Proveedor")]
    public string Telefono { get; set; } = null!;

    [Required(ErrorMessage ="Debe ingresar la Direccion del Proveedor")]
    public string Direccion { get; set; } = null!;

    [Range(0, double.MaxValue, ErrorMessage = "El balance no puede ser negativo")]
    public decimal Balance { get; set; } = 0;

    [Required(ErrorMessage ="Indique el Estado del Proveedor")]
    public bool Estado { get; set; }
}
