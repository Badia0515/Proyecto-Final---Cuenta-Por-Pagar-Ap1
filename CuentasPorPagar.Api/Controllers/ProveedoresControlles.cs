using CuentasPorPagar.Data.Models;
using CuentasPorPagar.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuentasPorPagar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController(ProveedoresService proveedoresService) : ControllerBase
    {
        // GET: api/Proveedores
        [HttpGet]
        public async Task<List<Proveedores>> GetProveedores()
        {
            var Result = await proveedoresService.GetList(P => true);
            return Result.Value;
        }

        // GET: api/Proveedores/5
        [HttpGet("{id}")]
        public async Task<Proveedores> GetProveedor(int id)
        {
            var Proveedor = await proveedoresService.Buscar(id);
            return Proveedor.Value;
        }

        // PUT: api/Proveedores/5
        [HttpPut("{id}")]
        public async Task<bool> PutProveedor(int id, Proveedores proveedor)
        {
            proveedor.ProveedorId = id;
            return await proveedoresService.Editar(proveedor);
        }

        // POST: api/Proveedores
        [HttpPost]
        public async Task<CreatedResult> PostProveedor(Proveedores proveedor)
        {
            var Result = await proveedoresService.Guardar(proveedor);
            return Created();
        }

        // DELETE: api/Proveedores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            var Result = await proveedoresService.Eliminar(id);
            return Ok(Result);
        }

    }
}
