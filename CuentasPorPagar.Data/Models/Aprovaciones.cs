using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuentasPorPagar.Data.Models;

public class Aprovaciones
{
    [Key]
    public int AprovacionId { get; set; }

    [Required(ErrorMessage ="Debe de completar este campo")]
    public int PagoId { get; set; }

    [Required(ErrorMessage ="Debe de completar este campo")]
    public DateOnly FechaAprobacion { get; set; }

    [Required(ErrorMessage ="Debe de completar este campo")]
    public int UsuarioId { get; set; } //De donde sale GerenteId

    [Required(ErrorMessage ="Debe de completar este campo")]
    public string Estado { get; set; } = null!;

    [Required(ErrorMessage ="Debe de completar este campo")]
    public string Comentario { get; set; } = null!;

    [ForeignKey("PagoId")]
    public virtual Pagos Pago { get; set; } = null!;

    [ForeignKey("UsuarioId")]
    public virtual Usuarios Usuario { get; set; } = null!;
}
