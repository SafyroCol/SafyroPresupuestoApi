using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;

namespace SafyroPresupuestos.Controllers
{
    [Authorize(Roles = "AdministradorEmpresa,CoordinadorDeProyecto,SupervisorDeObra")]
    [ApiController]
    [Route("api/[controller]")]
    public class BitacoraPresupuestoController : ControllerBase
    {
        private readonly IBitacoraPresupuestoService _service;

        public BitacoraPresupuestoController(IBitacoraPresupuestoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok( await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BitacoraPresupuesto entity) => Ok(await _service.CreateAsync(entity));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BitacoraPresupuesto entity) => Ok(await _service.UpdateAsync(entity));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await _service.DeleteAsync(id));
    }
}
