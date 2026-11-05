using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CuentasPorPagar.Data.Models
{
    public class Devoluciones
    {
        [Key]
        public int DevolucionId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una factura")]
        public int FacturasId { get; set; }

        [Required(ErrorMessage = "Debe ingresar la fecha de devolución")]
        public DateOnly Fecha { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Ingrese un monto válido")]
        public double Monto { get; set; }

        [Required(ErrorMessage = "Debe ingresar el motivo de la devolución")]
        public string Motivo { get; set; } = null!;

        [Required(ErrorMessage = "Establezca el estado de la devolución")]
        public string Estado { get; set; } = null!;

        [ForeignKey("FacturasId")]
        public virtual Facturas Factura { get; set; } = null!;
    }
}
