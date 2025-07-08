using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SafyroPresupuestos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "AdministradorEmpresa,CoordinadorDeProyecto,IngenieroDeCostos,SupervisorDeObra")]
    public class DocumentosController : ControllerBase
    {
        [HttpGet("listar")]
        public IActionResult ListarDocumentos()
        {
            return Ok(new { mensaje = "Listado de documentos disponible." });
        }
    }
}
