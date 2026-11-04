using System;
using System.Collections.Generic;
using System.Text;

namespace CuentasPorPagar.Data.Models;

public class Aprovaciones
{
    public int AprovacionId { get; set; }

    public int PagoId { get; set; }

    public DateOnly FechaAprobacion { get; set; }

    public int UsuarioId { get; set; } //De donde sale GerenteId

    public string Estado { get; set; } = null!;

    public string Comentario { get; set; } = null!;
}

