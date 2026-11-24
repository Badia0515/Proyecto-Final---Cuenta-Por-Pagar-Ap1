using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuentasPorPagar.Data.Models;

public class ProveedorDetalle
{
    [Key]
    public int DetalleId { get; set; }

    [Range(1,int.MaxValue,ErrorMessage ="")]
    public int ProveedorId { get; set;}

    [Range(1, int.MaxValue, ErrorMessage ="")]
    public int PagoId { get; set; }

    [ForeignKey("ProveedorId")]
    [InverseProperty("detalle")]
    public virtual Proveedores Proveedor { get; set; } = null!;

    [ForeignKey("PagoId")]
    public virtual Pagos Pago { get; set; } = null!;


}

