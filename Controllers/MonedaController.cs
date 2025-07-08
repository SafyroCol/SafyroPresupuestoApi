using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;

namespace SafyroPresupuestos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonedaController : ControllerBase
    {
        private readonly IMonedaService _service;

        public MonedaController(IMonedaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();

                if (result == null || !result.Any())
                {
                    return NotFound(new
                    {
                        ok = false,
                        message = "No se encontraron registros.",
                        contenido = Array.Empty<object>()
                    });
                }

                return Ok(new
                {
                    ok = true,
                    message = "Consulta exitosa.",
                    contenido = result
                });
            }
            catch (Exception ex)
            {
                // Opcional: Loggear el error con ILogger
                // _logger.LogError(ex, "Error en GetAll");

                return StatusCode(500, new
                {
                    ok = false,
                    message = "Ocurrió un error interno al obtener los datos.",
                    detalle = ex.Message
                });
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Moneda entity) => Ok(await _service.CreateAsync(entity));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Moneda entity) => Ok(await _service.UpdateAsync(id, entity));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await _service.DeleteAsync(id));
    }
}
