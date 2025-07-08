using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafyroPresupuestos.Data;
using SafyroPresupuestos.DTOs;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services;
using SafyroPresupuestos.Services.Interfaces;

namespace SafyroPresupuestos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class PresupuestoController : BaseController
    {
        private readonly IPresupuestoService _service;
        private readonly ILogger<PresupuestoController> _logger;
        private readonly ApplicationDbContext _context;
        public PresupuestoController(
            IPresupuestoService service,
            ILogger<PresupuestoController> logger,
            IPresupuestoService presupuestoService,
            ApplicationDbContext context)
    : base(presupuestoService)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var presupuestos = await _service.GetAll();
                return Ok(presupuestos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los presupuestos.");
                return StatusCode(500, "No se pudo recuperar la lista de presupuestos. Inténtelo más tarde.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _service.GetById(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Presupuestos entity) => Ok(await _service.Create(entity));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Presupuestos entity) => Ok(await _service.Update(id, entity));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await _service.Delete(id));

        [HttpGet("por-proyecto/{proyectoId}")]
        public async Task<IActionResult> ObtenerPorProyectoId(int proyectoId)
        {
            try
            {
                var presupuesto = await _service.ObtenerPorProyectoIdAsync(proyectoId);
                if (presupuesto == null)
                    return Ok(new ResponsePresupuestoDto { Ok = false, Message = "No se encontró un presupuesto para este proyecto." });

                return Ok(new ResponsePresupuestoDto { Ok = true, Contenido = presupuesto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponsePresupuestoDto
                {
                    Ok = false,
                    Message = $"Error interno al obtener presupuesto: {ex.Message}"
                });
            }
        }

        [HttpGet("por-empresa/{empresaId}")]
        public async Task<IActionResult> ObtenerProyectosPorEmpresa(int empresaId)
        {
            try
            {
                var proyectos = await _context.Proyectos
                    .Where(p => p.EmpresaId == empresaId)
                    .ToListAsync();

                if (proyectos != null)
                {
                    return Ok(new ResponseProyectoDto
                    {
                        Ok = true,
                        ContenidoList = proyectos
                    });
                }

                return Ok(new ResponseProyectoDto
                {
                    Ok = false
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseProyectoDto
                {
                    Ok = false,
                    Message = $"Error al obtener proyectos: {ex.Message}"
                });
            }
        }

        [HttpPost("crear-por-proyecto/{proyectoId}/{monedaId}")]
        public async Task<IActionResult> CrearPresupuestoPorProyecto(int proyectoId, int monedaId)
        {
            try
            {
                var proyecto = await _context.Proyectos.FindAsync(proyectoId);
                if (proyecto == null)
                    return NotFound(new ResponseProyectoDto { Ok = false, Message = "Proyecto no encontrado." });

                var existe = await _context.Presupuestos.AnyAsync(p => p.ProyectoId == proyectoId);
                if (existe)
                    return BadRequest(new ResponseProyectoDto { Ok = false, Message = "El proyecto ya tiene un presupuesto asignado." });

                var nuevo = new Presupuestos
                {
                    ProyectoId = proyectoId,
                    Nombre = $"Presupuesto de {proyecto.Nombre}",
                    Fecha = DateTime.UtcNow,
                    MonedaId = monedaId
                };

                _context.Presupuestos.Add(nuevo);
                await _context.SaveChangesAsync();

                return Ok(new ResponsePresupuestoDto { Ok = true, Contenido = new PresupuestoDto { Id = nuevo.Id, Nombre = nuevo.Nombre, ProyectoId = nuevo.ProyectoId } });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseProyectoDto
                {
                    Ok = false,
                    Message = $"Error al crear presupuesto: {ex.Message}"
                });
            }
        }
        [HttpPost("importar-excel")]
        public async Task<IActionResult> ImportarExcel([FromBody] PresupuestoImportarExcelDto importDto)
        {
            if (importDto.Data == null || !importDto.Data.Any() || importDto.Presupuesto == null)
                return BadRequest(new { ok = false, message = "Data o encabezado de presupuesto inválido" });

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. Crear el Presupuesto principal
                var presupuesto = new Presupuestos
                {
                    ProyectoId = importDto.Presupuesto.ProyectoId,
                    Nombre = importDto.Presupuesto.Nombre,
                    SubtotalMateriales = importDto.Presupuesto.CostoMateriales,
                    SubtotalManoObra = importDto.Presupuesto.CostoManoObra,
                    SubtotalEquipos = importDto.Presupuesto.CostoDepreciacion,
                    SubtotalOtros = importDto.Presupuesto.CostoOtros,
                    Indirectos = importDto.Presupuesto.CostoIndirectos,
                    MonedaId = importDto.Presupuesto.MonedaId,
                    Fecha = importDto.Presupuesto.FechaCreacion
                };

                _context.Presupuestos.Add(presupuesto);
                await _context.SaveChangesAsync();

                int presupuestoId = presupuesto.Id; // El nuevo ID

                // 2. Procesa las filas del Excel y agrégalas por rubro
                foreach (var row in importDto.Data)
                {
                    string rubro = row.ContainsKey("Rubro") ? row["Rubro"]?.ToString() : null;

                    if (string.IsNullOrEmpty(rubro)) continue;

                    switch (rubro.ToLower())
                    {
                        case "materiales":
                            var mat = new MaterialPresupuesto
                            {
                                PresupuestoId = presupuestoId,
                                MaterialId = Convert.ToInt32(row["MaterialId"]),
                                UnidadDeMedida = row["UnidadDeMedida"]?.ToString(),
                                Cantidad = Convert.ToDecimal(row["Cantidad"]),
                                CostoUnitario = Convert.ToDecimal(row["CostoUnitario"])
                            };
                            _context.MaterialPresupuesto.Add(mat);
                            break;

                        case "mano de obra":
                            var mo = new ManoObraPresupuesto
                            {
                                PresupuestoId = presupuestoId,
                                ManoObraId = Convert.ToInt32(row["ManoObraId"]),
                                // ... otros campos según tu modelo
                            };
                            _context.ManoObraPresupuesto.Add(mo);
                            break;

                        case "equipos":
                            var eq = new EquipoPresupuesto
                            {
                                PresupuestoId = presupuestoId,
                                EquipoDepreciacionId = Convert.ToInt32(row["EquipoDepreciacionId"]),
                                // ... otros campos si aplica
                            };
                            _context.EquipoPresupuesto.Add(eq);
                            break;

                            // Puedes agregar otros casos para servicios terceros y costos indirectos
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { ok = true, message = "Presupuesto y rubros importados correctamente" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new { ok = false, message = $"Error al importar: {ex.Message}" });
            }
        }

    }
}
