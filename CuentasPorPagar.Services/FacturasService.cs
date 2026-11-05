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
    public class FacturasService(IDbContextFactory<ContextBD> contextFactory):IServiceResult<Facturas, int>
    {
        public async Task<Result<Facturas?>> Buscar(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            return await context.facturas.FirstOrDefaultAsync(F => F.FacturasId == id);
        }

        public async Task<bool> Existe(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            return await context.facturas.AnyAsync(F => F.FacturasId == id);
        }

        public async Task<Result> Eliminar(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var Eliminado = await context.facturas.Where(F => F.FacturasId == id).ExecuteDeleteAsync() > 0;

            return Result.Success(Eliminado);
        }

        public async Task<Result<List<Facturas>>> GetList(
            Expression<Func<Facturas, bool>> Lista)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            return await context.facturas.Where(Lista).AsNoTracking().ToListAsync();
        }

        public async Task<bool> Insertar(Facturas facturas)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            context.facturas.Add(facturas);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Editar(Facturas facturas)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            context.facturas.Update(facturas);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<Result> Guardar(Facturas facturas)
        {
            bool Guardando;
            if (!await Existe(facturas.FacturasId))
            {
                Guardando = await Insertar(facturas);
            }
            else
            {
                Guardando = await Editar(facturas);
            }
            return Result.Success(Guardando);
        }
    }
}
