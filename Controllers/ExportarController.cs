using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SafyroPresupuestos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "AdministradorEmpresa,CoordinadorDeProyecto,IngenieroDeCostos,SupervisorDeObra,AnalistaFinanciero")]
    public class ExportarController : ControllerBase
    {
        [HttpGet("reportes")]
        public IActionResult ExportarReportes()
        {
            return Ok(new { mensaje = "Exportación de reportes realizada con éxito." });
        }
    }
}
