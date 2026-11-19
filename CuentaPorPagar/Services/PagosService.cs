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
    public class PagosService(IDbContextFactory<ContextBD> contextFactory):IServiceResult<Pagos, int>
    {
        public async Task<Result<Pagos?>> Buscar(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            return await context.pagos.FirstOrDefaultAsync(P => P.PagosId == id);
        }

        public async Task<bool> Existe(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            return await context.pagos.AnyAsync(P => P.PagosId == id);
        }

        public async Task<Result> Eliminar(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var Eliminado = await context.pagos.Where(P => P.PagosId == id).ExecuteDeleteAsync() > 0;

            return Result.Success(Eliminado);
        }

        public async Task<Result<List<Pagos>>> GetList(
            Expression<Func<Pagos, bool>> criterio)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            return await context.pagos.Where(criterio).AsNoTracking().ToListAsync();
        }

        public async Task<bool> Insertar(Pagos pagos)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            context.pagos.Add(pagos);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Editar(Pagos pagos)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            context.pagos.Update(pagos);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<Result> Guardar(Pagos pagos)
        {
            bool Guardando;
            if (!await Existe(pagos.PagosId))
            {
                Guardando = await Insertar(pagos);
            }
            else
            {
                Guardando = await Editar(pagos);
            }
            return Result.Success(Guardando);
        }

           
    }
}
