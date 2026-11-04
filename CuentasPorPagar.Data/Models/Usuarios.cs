using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuentasPorPagar.Data.Models;
public class Usuarios
{
    [Key]
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Rol { get; set; } = null!;

}

