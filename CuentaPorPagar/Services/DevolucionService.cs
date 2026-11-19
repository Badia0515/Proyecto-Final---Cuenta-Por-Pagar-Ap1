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
            return await context.devoluciones.FirstOrDefaultAsync(D => D.DevolucionId == id);
        }

        public async Task<bool> Existe(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            return await context.devoluciones.AnyAsync(D => D.DevolucionId == id);
        }

        public async Task<Result> Eliminar(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var Eliminado = await context.devoluciones.Where(D => D.DevolucionId == id).ExecuteDeleteAsync() > 0;

            return Result.Success(Eliminado);
        }

        public async Task<Result<List<Devoluciones>>> GetList(
            Expression<Func<Devoluciones, bool>> Lista)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            return await context.devoluciones.Where(Lista).AsNoTracking().ToListAsync();
        }

        public async Task<bool> Insertar(Devoluciones devolucion)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            context.devoluciones.Add(devolucion);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Editar(Devoluciones devolucion)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            context.devoluciones.Update(devolucion);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<Result> Guardar(Devoluciones devolucion)
        {
            bool Guardando;
            if (!await Existe(devolucion.FacturasId))
            {
                Guardando = await Insertar(devolucion);
            }
            else
            {
                Guardando = await Editar(devolucion);
            }
            return Result.Success(Guardando);
        }
    }
}
