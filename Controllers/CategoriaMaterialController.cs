using Microsoft.AspNetCore.Mvc;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;

namespace SafyroPresupuestos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaMaterialController : ControllerBase
    {
        private readonly ICategoriaMaterialService _service;

        public CategoriaMaterialController(ICategoriaMaterialService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoriaMaterial entity) => Ok(await _service.CreateAsync(entity));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoriaMaterial entity) => Ok(await _service.UpdateAsync(id, entity));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await _service.DeleteAsync(id));
    }
}
