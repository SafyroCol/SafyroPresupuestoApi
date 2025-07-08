using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SafyroPresupuestos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var username = User.Identity?.Name;
            var email = User.FindFirstValue(ClaimTypes.Email);
            var empresaId = User.FindFirstValue("EmpresaId");
            var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

            return Ok(new
            {
                Autenticado = true,
                Usuario = username,
                Email = email,
                EmpresaId = empresaId,
                Roles = roles
            });
        }
    }
}
