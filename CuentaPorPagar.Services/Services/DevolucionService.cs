using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Aplicada1.Core;
using CuentasPorPagar.Data;
using CuentasPorPagar.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CuentasPorPagar.Services
{
    public class DevolucionesService(IDbContextFactory<ContextBD> contextFactory): IServiceResult<Devoluciones, int>
    {
        public async Task<Result<Devoluciones?>> Buscar(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            return await context.devoluciones
                 .Include(d => d.Factura)
                 .ThenInclude(f => f.Proveedor)
                 .FirstOrDefaultAsync(D => D.DevolucionId == id);
        }

        public async Task<bool> Existe(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            return await context.devoluciones.AnyAsync(D => D.DevolucionId == id);
        }

        public async Task<Result> Eliminar(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var devolucion = await context.devoluciones
                .Include(d => d.Factura)
                .FirstOrDefaultAsync(d => d.DevolucionId == id);
            if (devolucion == null)
            {
                return Result.Failure(new Error("Devolucion.NoEncontrada", "La devolucion no fue encontrada."));
            }
            int proveedorId = devolucion.Factura?.ProveedorId ?? 0;
            if (proveedorId > 0)
            {
                await AfectarBalance(proveedorId, devolucion.Monto, Operacion.Suma);
            }
            if (devolucion.Factura != null)
            {
                devolucion.Factura.Estado = "Pagada";
            }

            context.devoluciones.Remove(devolucion);
            var eliminado = await context.SaveChangesAsync() > 0;
            return Result.Success(eliminado);
        }

        public async Task<Result<List<Devoluciones>>> GetList(
            Expression<Func<Devoluciones, bool>> Lista)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            return await context.devoluciones
                 .Include(d => d.Factura)
                 .ThenInclude(f => f.Proveedor)
                 .Where(Lista)
                 .AsNoTracking()
                 .ToListAsync();
        }

        public async Task<bool> Insertar(Devoluciones devolucion)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var factura = await context.Facturas
                .FirstOrDefaultAsync(f => f.FacturasId == devolucion.FacturasId);

            if (factura == null)
                return false;

            devolucion.Factura = null!;
            factura.Estado = "Devuelto";
            context.devoluciones.Add(devolucion);

            var guardado = await context.SaveChangesAsync() > 0;
            if (!guardado)
            {
                return false;

            }
                

            await AfectarBalance(factura.ProveedorId, devolucion.Monto, Operacion.Resta);
            return true;
        }

        public async Task<bool> Editar(Devoluciones devolucion)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            var anterior = await context.devoluciones
                .AsNoTracking()
                .Include(d => d.Factura)
                .FirstOrDefaultAsync(d => d.DevolucionId == devolucion.DevolucionId);
            var facturaNueva = await context.Facturas
                .FirstOrDefaultAsync(f => f.FacturasId == devolucion.FacturasId);

            if (anterior != null && anterior.Factura != null)
            {
                await AfectarBalance(anterior.Factura.ProveedorId, anterior.Monto, Operacion.Suma);
            }

            if (facturaNueva != null)
            {
                await AfectarBalance(facturaNueva.ProveedorId, devolucion.Monto, Operacion.Resta);
                facturaNueva.Estado = "Devuelto";
            }

            devolucion.Factura = null!;
            context.devoluciones.Update(devolucion);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<Result> Guardar(Devoluciones devolucion)
        {
            bool Guardando;
            if (devolucion.DevolucionId == 0)
            {
                Guardando = await Insertar(devolucion);
            }
            else
            {
                Guardando = await Editar(devolucion);
            }

            return Result.Success(Guardando);
        }
        private async Task AfectarBalance(int proveedorId, double monto, Operacion operacion)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var proveedor = await context.Proveedores
                .FirstOrDefaultAsync(p => p.ProveedorId == proveedorId);

            if (proveedor != null)
            {
                if (operacion == Operacion.Suma)
                    proveedor.Balance += monto;
                else
                    proveedor.Balance -= monto;

                await context.SaveChangesAsync();
            }
        }

        public enum Operacion
        {
            Suma = 1,
            Resta = 2
        }
    }
}
