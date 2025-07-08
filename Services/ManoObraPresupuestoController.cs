
using Microsoft.AspNetCore.Mvc;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;
using System.Threading.Tasks;

namespace SafyroPresupuestos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManoObraPresupuestoController : ControllerBase
    {
        private readonly IManoObraPresupuestoService _service;

        public ManoObraPresupuestoController(IManoObraPresupuestoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ManoObraPresupuesto model)
        {
            await _service.CreateAsync(model);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ManoObraPresupuesto model)
        {
            if (id != model.Id) return BadRequest();
            await _service.UpdateAsync(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
