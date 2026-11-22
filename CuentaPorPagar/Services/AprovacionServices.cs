using Microsoft.EntityFrameworkCore;
using Aplicada1.Core;
using CuentasPorPagar.Data;
using CuentasPorPagar.Data.Models;
using System.Linq.Expressions;

namespace CuentasPorPagar.Services;

public class AprovacionServices(IDbContextFactory<ContextBD> contextFactory) : IServiceResult<Aprovaciones, int>
{
    // Guardar (inserta o actualiza)
    public async Task<Result> Guardar(Aprovaciones entidad)
    {
        if (!await Existe(entidad.AprovacionId))
        {
            var inserto = await Insertar(entidad);
            return Result.Success(inserto);
        }
        else
        {
            var modifica = await Modificar(entidad);
            return Result.Success(modifica);
        }
    }

    // Verifica existencia por Id
    private async Task<bool> Existe(int? aprobId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Aprovaciones.AnyAsync(a => a.AprovacionId == aprobId);
    }

    // Inserta una nueva aprobación
    private async Task<bool> Insertar(Aprovaciones entidad)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Aprovaciones.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    // Modifica una aprobación existente
    private async Task<bool> Modificar(Aprovaciones entidad)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Aprovaciones.Update(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    // Buscar por Id con navigation properties
    public async Task<Result<Aprovaciones?>> Buscar(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Aprovaciones
            .Include(a => a.Pago)
            .Include(a => a.Usuario)
            .FirstOrDefaultAsync(a => a.AprovacionId == id);
    }

    // Listado con criterio
    public async Task<Result<List<Aprovaciones>>> GetList(Expression<Func<Aprovaciones, bool>> criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Aprovaciones
            .Include(a => a.Pago)
            .Include(a => a.Usuario)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    // Eliminar una aprobación
    public async Task<Result> Eliminar(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var eliminado = await contexto.Aprovaciones
            .Where(a => a.AprovacionId == id)
            .ExecuteDeleteAsync() > 0;
        return Result.Success(eliminado);
    }
}
