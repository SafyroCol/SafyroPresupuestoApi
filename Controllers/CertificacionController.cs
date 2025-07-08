using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SafyroPresupuestos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "AdministradorEmpresa,CoordinadorDeProyecto,IngenieroDeCostos")]
    public class CertificacionController : ControllerBase
    {
        [HttpGet("detalle")]
        public IActionResult ObtenerCertificaciones()
        {
            return Ok(new { mensaje = "Certificaciones obtenidas correctamente." });
        }
    }
}
