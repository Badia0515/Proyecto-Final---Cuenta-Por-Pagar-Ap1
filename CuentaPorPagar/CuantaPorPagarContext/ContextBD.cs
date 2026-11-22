using CuentaPorPagar.Data.Models;
using CuentasPorPagar.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CuentasPorPagar.Data
{
    public class ContextBD : DbContext 
    {
        public ContextBD(DbContextOptions<ContextBD>options): base(options)
        {
        }
        public virtual DbSet<Proveedores> Proveedores { get; set; }

        public virtual DbSet<Facturas> Facturas { get; set; }
        public virtual DbSet<FacturaDetalle> FacturaDetalle { get; set; }

        public virtual DbSet<Pagos> pagos{ get; set; }

        public virtual DbSet<Devoluciones> devoluciones { get; set; }

        public virtual DbSet<Transacciones> transacciones { get; set; }
        public virtual DbSet<ProveedorDetalle> ProveedorDetalle { get; set; }

        public virtual DbSet<Aprovaciones> Aprovaciones { get; set; }
    }
}
