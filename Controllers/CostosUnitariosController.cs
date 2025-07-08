using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SafyroPresupuestos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "AdministradorEmpresa,IngenieroDeCostos")]
    public class CostosUnitariosController : ControllerBase
    {
        [HttpGet("listado")]
        public IActionResult ObtenerCostosUnitarios()
        {
            return Ok(new { mensaje = "Listado de Costos Unitarios generado correctamente." });
        }
    }
}
