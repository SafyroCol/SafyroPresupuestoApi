using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;

namespace SafyroPresupuestos.Controllers
{
    [Authorize(Roles = "AdministradorEmpresa,CoordinadorDeProyecto,IngenieroDeCostos,SupervisorDeObra")]
    [ApiController]
    [Route("api/[controller]")]
    public class ManoObraController : ControllerBase
    {
        private readonly IManoObraService _service;

        public ManoObraController(IManoObraService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _service.GetById(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ManoObra entity) => Ok(await _service.Create(entity));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ManoObra entity) => Ok(await _service.Update(id, entity));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await _service.Delete(id));
    }
}
