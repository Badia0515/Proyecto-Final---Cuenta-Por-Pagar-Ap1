using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
        
    }
}
