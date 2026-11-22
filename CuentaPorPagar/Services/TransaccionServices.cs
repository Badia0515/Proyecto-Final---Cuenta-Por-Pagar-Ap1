using System.Linq.Expressions;
using Aplicada1.Core;
using CuentasPorPagar.Data;
using CuentasPorPagar.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CuentasPorPagar.Services
{
    public class TransaccionServices(IDbContextFactory<ContextBD> contextFactory) : IServiceResult<Transacciones, int>
    {
        public async Task<Result<Transacciones?>> Buscar(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            return await context.transacciones.FirstOrDefaultAsync(T => T.TransaccionId == id);
        }
        public async Task<bool> Existe(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            return await context.transacciones.AnyAsync(T => T.TransaccionId == id);
        }

        public async Task<Result> Eliminar(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            var Eliminado = await context.transacciones.Where(T => T.TransaccionId == id).ExecuteDeleteAsync() > 0;

            return Result.Success(Eliminado);
        }

        public async Task<Result<List<Transacciones>>> GetList(Expression<Func<Transacciones, bool>> criterio)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            return await context.transacciones.Where(criterio).AsNoTracking().ToListAsync();
        }
        public async Task<bool> Insertar(Transacciones transacciones)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            context.transacciones.Add(transacciones);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Editar(Transacciones transacciones)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            context.transacciones.Update(transacciones);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<Result> Guardar(Transacciones entidad)
        {
            bool Guardando;

            if (!await Existe(entidad.TransaccionId))
            {
                Guardando = await Insertar(entidad);
            }
            else
            {
                Guardando = await Editar(entidad);
            }
            return Result.Success(Guardando);
        }
    }
}
