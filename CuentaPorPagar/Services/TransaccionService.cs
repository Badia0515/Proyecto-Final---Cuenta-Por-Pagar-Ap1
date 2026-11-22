using Microsoft.EntityFrameworkCore;
using Aplicada1.Core;
using CuentasPorPagar.Data;
using CuentasPorPagar.Data.Models;
using System.Linq.Expressions;

namespace CuentasPorPagar.Services;

public class TransaccionService(IDbContextFactory<ContextBD> contextFactory) : IServiceResult<Transacciones, int>
{
    // Guardar (inserta o actualiza)
    public async Task<Result> Guardar(Transacciones entidad)
    {
        if (!await Existe(entidad.TransaccionId))
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
    private async Task<bool> Existe(int? transaccionId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Transacciones.AnyAsync(t => t.TransaccionId == transaccionId);
    }

    // Inserta una nueva transacción
    private async Task<bool> Insertar(Transacciones entidad)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Transacciones.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    // Modifica una transacción existente
    private async Task<bool> Modificar(Transacciones entidad)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Transacciones.Update(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    // Buscar por Id
    public async Task<Result<Transacciones?>> Buscar(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Transacciones
            .Include(t => t.Usuario)
            .Include(t => t.Pago)
            .FirstOrDefaultAsync(t => t.TransaccionId == id);
    }

    // Listado con criterio
    public async Task<Result<List<Transacciones>>> GetList(Expression<Func<Transacciones, bool>> criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Transacciones
            .Include(t => t.Usuario)
            .Include(t => t.Pago)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    // Eliminar una transacción
    public async Task<Result> Eliminar(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var eliminado = await contexto.Transacciones
            .Where(t => t.TransaccionId == id)
            .ExecuteDeleteAsync() > 0;
        return Result.Success(eliminado);
    }
}
