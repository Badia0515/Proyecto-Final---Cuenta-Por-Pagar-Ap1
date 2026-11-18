using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuentasPorPagar.Data.Models;

public class Pagos
{
    [Key]
    public int PagosId { get; set; }

    [Required(ErrorMessage ="Este campo es obligatorio")]
    public int FacturaId { get; set; }

    [Required(ErrorMessage ="Seleccione una fecha para el Pago")]
    public DateOnly FechaProgramada { get; set; }

    [Range(1, double.MaxValue, ErrorMessage ="Ingrese un monto valido")]
    public double Monto { get; set; }

    [Required(ErrorMessage ="Establezca el estado del Pago")]
    public string Estado { get; set; } = null!;

    [Required(ErrorMessage ="Debe ingresar una Referencia")]
    public string Referencia { get; set; } = null!;
}

