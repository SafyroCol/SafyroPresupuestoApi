using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafyroPresupuestos.DTOs;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;

namespace SafyroPresupuestos.Controllers
{
    [Authorize(Roles = "AdministradorEmpresa,CoordinadorDeProyecto,IngenieroDeCostos")]
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController : BaseController
    {
        private readonly IMaterialService _service;
        private readonly IPresupuestoService _presuservice;

        public MaterialController(IMaterialService service, IPresupuestoService presupuestoService)
    : base(presupuestoService)
        {
            _service = service;
            _presuservice = presupuestoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _service.GetById(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Material entity) => Ok(await _service.Create(entity));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Material entity) => Ok(await _service.Update(id, entity));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await _service.Delete(id));

        // ✅ Obtener todos los materiales presupuestados y sus costos reales (si existen)
        [HttpGet("presupuesto/{presupuestoId}")]
        public async Task<IActionResult> GetMaterialesConCostos(int presupuestoId)
        {
            if (!ValidarEmpresaPorPresupuesto(presupuestoId))
                return Forbid("No tiene acceso a este presupuesto.");

            var materiales = await _presuservice.ObtenerMaterialesConCostosAsync(presupuestoId);
            return Ok(materiales);
        }

        // ✅ Registrar o actualizar los costos reales de los materiales
        [HttpPost("registrar-costos-reales")]
        public async Task<IActionResult> RegistrarCostosReales([FromBody] CostosRealesMaterialDto dto)
        {
            if (!ValidarEmpresaPorPresupuesto(dto.PresupuestoId))
                return Forbid("No tiene acceso a este presupuesto.");

            var resultado = await _presuservice.RegistrarCostosRealesAsync(dto);
            return Ok(resultado);
        }
    }
}
