using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;

namespace SafyroPresupuestos.Controllers
{
    [Authorize(Roles = "AdministradorEmpresa,IngenieroDeCostos")]
    [ApiController]
    [Route("api/[controller]")]
    public class EquipoController : ControllerBase
    {
        private readonly IEquipoDepreciacionService _service;

        public EquipoController(IEquipoDepreciacionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _service.GetById(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EquipoDepreciacion entity) => Ok(await _service.Create(entity));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EquipoDepreciacion entity) => Ok(await _service.Update(id, entity));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await _service.Delete(id));
    }
}
