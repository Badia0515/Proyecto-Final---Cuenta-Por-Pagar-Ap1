using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CuentasPorPagar.Data.Models
{
    public class FacturaDetalle
    {
        [Key]
        public int DetalleFacturaId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Seleccione una factura")]
        public int FacturasId { get; set; }

        [Required(ErrorMessage = "Ingrese la descripcion")]
        public string Descripcion { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Ingrese una cantidad valida")]
        public int Cantidad { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Ingrese un precio valido")]
        public decimal Precio { get; set; }

        [ForeignKey("FacturasId")]
        public virtual Facturas factura { get; set; } = null!;
    }
}
