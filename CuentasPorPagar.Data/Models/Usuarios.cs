using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuentasPorPagar.Data.Models;
public class Usuarios
{
    [Key]
    public int UsuarioId { get; set; }

    [Required(ErrorMessage ="Este campo es obligatorio")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage ="Este campo es obligatorio")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage ="Este campo es obligatorio")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage ="Este campo es obligatorio")]
    public string Rol { get; set; } = null!;

}

