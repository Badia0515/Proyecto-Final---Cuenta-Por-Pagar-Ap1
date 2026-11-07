using CuentasPorPagar.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CuentasPorPagar.Data
{
    public class ContextBD : DbContext 
    {
        public ContextBD(DbContextOptions<ContextBD>options): base(options)
        {
        }
        public virtual DbSet<Proveedores> proveedores { get; set; }

        public virtual DbSet<Facturas>facturas { get; set; }

        public virtual DbSet<Pagos> pagos{ get; set; }

        public virtual DbSet<Devoluciones> devoluciones { get; set; }
    }
}
