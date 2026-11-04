using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuentasPorPagar.Data.Models;

public class Transacciones
{
    [Key]
    public int TransaccionId { get; set; }

    [Required(ErrorMessage ="Debe completar este campo")]
    public int PagoId { get; set; }

    [Required(ErrorMessage ="Debe completar este campo")]
    public int UsuarioId { get; set; }

    [Required(ErrorMessage ="Debe completar este campo")]
    public string Accion { get; set; } = null!;

    [Required(ErrorMessage ="Debe completar este campo")]
    public DateOnly Fecha { get; set; }

    [Required(ErrorMessage ="Debe completar este campo")]
    public string Detalle { get; set; } = null!;

    [ForeignKey("UsuarioId")]
    public virtual Usuarios Usuario { get; set; } = null!;

    [ForeignKey("PagoId")]
    public virtual Pagos Pago { get; set; } = null!;
}

