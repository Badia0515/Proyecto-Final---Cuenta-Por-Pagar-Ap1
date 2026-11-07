using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuentasPorPagar.Data.Models;

public class Facturas {
    [Key]
    public int FacturasId { get; set; }

   [Range(1, int.MaxValue, ErrorMessage ="Ingrese un numero de Factura Valido")]
    public int NumFactura { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Ingrese un id de proveedor Valido")]
    public int ProveedorId { get; set; }

    [Required(ErrorMessage ="Este campo es obligatorio")]
    public DateOnly Fecha { get; set; }

    [Required(ErrorMessage="Este campo es obligatorio")]
    public string? Estado { get; set; }

    [ForeignKey("ProveedorId")]
    public virtual Proveedores Proveedor { get; set; } = null!;

    public virtual ICollection<FacturaDetalle> Detalles { get; set; } = new List<FacturaDetalle>();

}

