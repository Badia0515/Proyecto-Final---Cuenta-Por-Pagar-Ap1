using Microsoft.EntityFrameworkCore;
using CuentasPorPagar.Data;
using CuentasPorPagar.Data.Models;
using Aplicada1.Core;
using System.Linq.Expressions;

namespace CuentasPorPagar.Services;

public class FacturaServices(IDbContextFactory<ContextBD> contextFactory) : IServiceResult<Facturas, int>
{
    public async Task<Result> Guardar(Facturas factura)
    {
        if (!await Existe(factura.FacturasId))
        {
            var inserto = await Insertar(factura);
            return Result.Success(inserto);
        }
        else
        {
            var modifica = await Modificar(factura);
            return Result.Success(modifica);
        }
    }

    public async Task<bool> Existe(int facturaId)
    {
        await using var ctx = await contextFactory.CreateDbContextAsync();
        return await ctx.facturas.AnyAsync(f => f.FacturasId == facturaId);
    }

    private async Task<bool> Insertar(Facturas factura)
    {
        await using var ctx = await contextFactory.CreateDbContextAsync();
        // evitar referencias circulares
        foreach (var det in factura.Detalles)
        {
            det.factura = null;
        }
        ctx.facturas.Add(factura);
        await AfectarProveedor(factura.Detalles.ToArray(), Operacion.Suma);
        return await ctx.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Facturas factura)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var anterior = await contexto.facturas
            .Include(f => f.Detalles)
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.FacturasId == factura.FacturasId);

        if (anterior != null)
        {
            await AfectarProveedor(anterior.Detalles.ToArray(), Operacion.Resta);
        }

        await AfectarProveedor(factura.Detalles.ToArray(), Operacion.Suma);
        // eliminar detalles antiguos
        await contexto.facturaDetalle.Where(d => d.FacturasId == factura.FacturasId).ExecuteDeleteAsync();
        foreach (var item in factura.Detalles)
        {
            item.factura = null;
            item.FacturasId = 0;
        }
        contexto.facturas.Update(factura);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task AfectarProveedor(FacturaDetalle[] detalle, Operacion operacion)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        foreach (var d in detalle)
        {
            var afectar = await contexto.proveedores.SingleAsync(p => p.ProveedorId == d.ProveedorId);
            if (operacion == Operacion.Suma)
                afectar.Balance += d.Precio;
            else
                afectar.Balance -= d.Precio;
        }
        await contexto.SaveChangesAsync();
    }

    public async Task<Result<Facturas?>> Buscar(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.facturas
            .Include(f => f.Detalles)
            .Include(f => f.Proveedor)
            .FirstOrDefaultAsync(f => f.FacturasId == id);
    }

    public async Task<Result<List<Facturas>>> GetList(Expression<Func<Facturas, bool>> criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.facturas
            .Include(f => f.Detalles)
            .Include(d => d.Proveedor)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Result> Eliminar(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var factura = await contexto.facturas
            .Include(f => f.Detalles)
            .FirstOrDefaultAsync(f => f.FacturasId == id);

        if (factura == null)
            return Result.Failure(new Error("Factura.NoEncontrada", "La factura no fue encontrada."));

        await AfectarProveedor(factura.Detalles.ToArray(), Operacion.Resta);
        contexto.facturaDetalle.RemoveRange(factura.Detalles);
        contexto.facturas.Remove(factura);
        var cant = await contexto.SaveChangesAsync();
        return Result.Success(cant);
    }

    public async Task<List<Facturas>> Listar(Expression<Func<Facturas, bool>> criterio)
    {
        await using var ctx = await contextFactory.CreateDbContextAsync();
        return await ctx.facturas
            .Include(f => f.Detalles)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public enum Operacion
    {
        Suma = 1,
        Resta = 2
    }
}
