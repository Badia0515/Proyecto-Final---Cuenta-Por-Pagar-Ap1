using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuentasPorPagar.Data.Models;

public class Transacciones
{
    public int TransaccionId { get; set; }

    public int PagoId { get; set; }

    public int UsuarioId { get; set; }

    public string Accion { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public string Detalle { get; set; } = null!;
}

