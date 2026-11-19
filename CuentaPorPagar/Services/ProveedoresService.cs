using Microsoft.EntityFrameworkCore;
using CuentasPorPagar.Data;
using Aplicada1.Core;
using CuentasPorPagar.Data.Models;
using System.Linq.Expressions;

namespace CuentasPorPagar.Services
{
    public class ProveedoresService(IDbContextFactory<ContextBD> contextFactory) : IServiceResult<Proveedores, int>
    {
        public async Task<Result<Proveedores?>> Buscar(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            return await context.proveedores.FirstOrDefaultAsync(P => P.ProveedorId == id);
        }

        public async Task<bool> Existe(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            return await context.proveedores.AnyAsync(P => P.ProveedorId == id);
        }

        public async Task<Result> Eliminar(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            var eliminado = await context.proveedores.Where(P => P.ProveedorId == id).ExecuteDeleteAsync() > 0;
            return Result.Success(eliminado);
        }

        public async Task<Result<List<Proveedores>>> GetList(Expression<Func<Proveedores, bool>> criterio)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            return await context.proveedores.Where(criterio).AsNoTracking().ToListAsync();
        }

        public async Task<bool> Insertar(Proveedores entidad)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            context.proveedores.Add(entidad);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Editar(Proveedores entidad)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            context.proveedores.Update(entidad);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<Result> Guardar(Proveedores entidad)
        {
            bool guardando;
            if (!await Existe(entidad.ProveedorId))
            {
                guardando = await Insertar(entidad);
            }
            else
            {
                guardando = await Editar(entidad);
            }

            return Result.Success(guardando);
        }
    }
}
