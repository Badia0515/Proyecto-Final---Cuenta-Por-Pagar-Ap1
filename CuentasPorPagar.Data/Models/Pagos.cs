using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuentasPorPagar.Data.Models;

public class Pagos
{
    public int PagosId { get; set; }

    public int FacturaId { get; set; }

    public DateOnly FechaProgramada { get; set; }

    public double Monto { get; set; }

    public string Estado { get; set; } = null!;

    public string Referencia { get; set; } = null!;
}

