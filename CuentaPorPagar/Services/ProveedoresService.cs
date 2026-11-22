using Microsoft.EntityFrameworkCore;
using Aplicada1.Core;
using CuentasPorPagar.Data;
using CuentasPorPagar.Data.Models;
using System.Linq.Expressions;

namespace CuentasPorPagar.Services;

public class ProveedoresService(IDbContextFactory<ContextBD> contextFactory) : IServiceResult<Proveedores, int>
{
    // Guardar (Insertar o Modificar)
    public async Task<Result> Guardar(Proveedores entidad)
    {
        if (!await Existe(entidad.ProveedorId))
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

    // Existencia
    public async Task<bool> Existe(int proveedorId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Proveedores.AnyAsync(p => p.ProveedorId == proveedorId);
    }

    // Insertar
    private async Task<bool> Insertar(Proveedores entidad)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        foreach (var det in entidad.detalle)
        {
            det.Proveedor = null;
        }
        contexto.Proveedores.Add(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    // Modificar
    private async Task<bool> Modificar(Proveedores entidad)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
       
        var anterior = await contexto.Proveedores
            .Include(p => p.detalle)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProveedorId == entidad.ProveedorId);

        if (anterior != null)
        {
            await contexto.ProveedorDetalle
                .Where(d => d.ProveedorId == entidad.ProveedorId)
                .ExecuteDeleteAsync();
        }

        
        foreach (var det in entidad.detalle)
        {
            det.Proveedor = null;
            det.DetalleId = 0;
        }

        contexto.Proveedores.Update(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Result<Proveedores?>> Buscar(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Proveedores
            .Include(p => p.detalle)
            .ThenInclude(d => d.Pago)
            .FirstOrDefaultAsync(p => p.ProveedorId == id);
    }

    // Listado con criterio (GetList)
    public async Task<Result<List<Proveedores>>> GetList(Expression<Func<Proveedores, bool>> criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Proveedores
            .Include(p => p.detalle)
            .ThenInclude(d => d.Pago)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    // Eliminación
    public async Task<Result> Eliminar(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var proveedor = await contexto.Proveedores
            .Include(p => p.detalle)
            .FirstOrDefaultAsync(p => p.ProveedorId == id);

        if (proveedor == null)
            return Result.Failure(new Error("Proveedor.NoEncontrado", "El proveedor no fue encontrado."));

        contexto.ProveedorDetalle.RemoveRange(proveedor.detalle);
        contexto.Proveedores.Remove(proveedor);
        var cant = await contexto.SaveChangesAsync();
        return Result.Success(cant);
    }

    // Listado simple (Listar)
    public async Task<List<Proveedores>> Listar(Expression<Func<Proveedores, bool>> criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Proveedores
            .Include(p => p.detalle)
            .ThenInclude(d => d.Pago)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
