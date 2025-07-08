using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SafyroPresupuestos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "AdministradorEmpresa,CoordinadorDeProyecto,AnalistaFinanciero")]
    public class GerencialController : ControllerBase
    {
        [HttpGet("resumen")]
        public IActionResult ObtenerResumenGerencial()
        {
            return Ok(new { mensaje = "Resumen gerencial cargado exitosamente." });
        }
    }
}
