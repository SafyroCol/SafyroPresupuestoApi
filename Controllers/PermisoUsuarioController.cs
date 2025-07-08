using Microsoft.AspNetCore.Mvc;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;

namespace SafyroPresupuestos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermisoUsuarioController : ControllerBase
    {
        private readonly IPermisoUsuarioService _service;

        public PermisoUsuarioController(IPermisoUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PermisoUsuario entity) => Ok(await _service.CreateAsync(entity));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PermisoUsuario entity) => Ok(await _service.UpdateAsync(id, entity));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await _service.DeleteAsync(id));
    }
}
