using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafyroPresupuestos.Data;
using SafyroPresupuestos.DTOs;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafyroPresupuestos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class PresupuestoController : ControllerBase
    {
        private readonly IPresupuestoService _service;
        private readonly ILogger<PresupuestoController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly string _carpetaEvidencias;

        public PresupuestoController(
            IPresupuestoService service,
            ILogger<PresupuestoController> logger,
            ApplicationDbContext context,
            IWebHostEnvironment env )
        {
            _service = service;
            _logger = logger;
            _context = context;
            _carpetaEvidencias = Path.Combine(env.WebRootPath ?? env.ContentRootPath, "uploads", "evidencias");
            if (!Directory.Exists(_carpetaEvidencias))
                Directory.CreateDirectory(_carpetaEvidencias);
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
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _service.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Presupuestos entity)
        {
            return Ok(await _service.Create(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Presupuestos entity)
        {
            return Ok(await _service.Update(id, entity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.Delete(id));
        }

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

                return Ok(new ResponsePresupuestoDto
                {
                    Ok = true,
                    Contenido = new PresupuestoDto { Id = nuevo.Id, Nombre = nuevo.Nombre, ProyectoId = nuevo.ProyectoId }
                });
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
                var proyecto = await _context.Proyectos.FirstOrDefaultAsync(m => m.Id == importDto.Presupuesto.ProyectoId);
                if (proyecto == null)
                    return BadRequest(new { ok = false, message = "Proyecto no existe" });

                // 1. Crear y guardar el Presupuesto principal
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

                int presupuestoId = presupuesto.Id;
                if (presupuestoId <= 0)
                    throw new Exception("No se pudo obtener el Id del presupuesto guardado.");

                var itemsImportados = new List<ItemPresupuesto>();

                // 2. Cargar la data recibida a la tabla ItemPresupuesto (staging)
                foreach (var row in importDto.Data)
                {
                    var item = new ItemPresupuesto
                    {
                        Id = Guid.NewGuid(),
                        PresupuestoId = presupuestoId,
                        Codigo = row.GetValueOrDefault("Código")?.ToString() ?? string.Empty,
                        Descripcion = row.GetValueOrDefault("Descripción del ítem")?.ToString() ?? string.Empty,
                        Unidad = row.GetValueOrDefault("Unidad")?.ToString() ?? string.Empty,
                        Rubro = row.GetValueOrDefault("Capítulo / Rubro")?.ToString() ?? string.Empty,
                        Tamaño = row.GetValueOrDefault("Tamaño")?.ToString() ?? string.Empty,
                        Observaciones = row.GetValueOrDefault("Especificaciones / Observaciones")?.ToString() ?? string.Empty
                    };
                    try
                    {
                        item.Cantidad = decimal.TryParse(row.GetValueOrDefault("Cantidad")?.ToString(), out var c) ? c : throw new Exception("Error en campo 'Cantidad'");
                        item.ValorUnitario = decimal.TryParse(row.GetValueOrDefault("Valor Unitario")?.ToString(), out var vu) ? vu : throw new Exception("Error en campo 'Valor Unitario'");
                        item.ValorTotal = decimal.TryParse(row.GetValueOrDefault("Valor Total")?.ToString(), out var vt) ? vt : throw new Exception("Error en campo 'Valor Total'");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Error procesando el campo 'Cantidad': {ex.Message}");
                    }
                    itemsImportados.Add(item);
                    _context.ItemPresupuesto.Add(item);
                }
                await _context.SaveChangesAsync();


                // 3. Procesa los ítems y llena las tablas específicas por rubro
                foreach (var item in itemsImportados)
                {
                    var descripcion = string.IsNullOrWhiteSpace(item.Descripcion) ? "No aplica" : item.Descripcion;
                    var rubro = (item.Rubro ?? string.Empty).Trim();

                    switch (rubro)
                    {
                        case "Materiales":
                            var material = await GetOrCreateMaterialAsync(
                                nombre: descripcion,
                                costoUnitario: item.ValorUnitario,
                                empresaId: proyecto.EmpresaId
                            );
                            var matPres = new MaterialPresupuesto
                            {
                                Id = Guid.NewGuid(),
                                PresupuestoId = presupuestoId,
                                MaterialId = material.Id,
                                Codigo = item.Codigo,
                                Descripcion = descripcion,
                                Unidad = item.Unidad,
                                Cantidad = item.Cantidad,
                                ValorUnitario = item.ValorUnitario,
                                ValorTotal = item.ValorTotal,
                                Rubro = item.Rubro ?? string.Empty,
                                Tamaño = item.Tamaño,
                                Observaciones = item.Observaciones
                            };
                            _context.MaterialPresupuesto.Add(matPres);
                            await _context.SaveChangesAsync();
                            break;

                        case "Mano de Obra":
                            var manoObra = await GetOrCreateManoObraAsync(
                                descripcion: descripcion,
                                costoHora: item.ValorUnitario,
                                empresaId: proyecto.EmpresaId,
                                tipoManoObraId: 1
                            );
                            var moPres = new ManoObraPresupuesto
                            {
                                Id = Guid.NewGuid(),
                                PresupuestoId = presupuestoId,
                                ManoObraId = manoObra.Id,
                                Codigo = item.Codigo,
                                Descripcion = descripcion,
                                Unidad = item.Unidad,
                                Cantidad = item.Cantidad,
                                ValorUnitario = item.ValorUnitario,
                                ValorTotal = item.ValorTotal,
                                Rubro = item.Rubro ?? string.Empty,
                                Observaciones = item.Observaciones
                            };
                            _context.ManoObraPresupuesto.Add(moPres);
                            await _context.SaveChangesAsync();
                            break;

                        case "Equipos":
                            var equipo = await GetOrCreateEquipoDepreciacionAsync(
                                nombreEquipo: item.Descripcion,
                                valorInicial: item.ValorUnitario * item.Cantidad,
                                valorResidual: 0,
                                vidaUtilMeses: 1,
                                empresaId: proyecto.EmpresaId
                            );
                            var eqPres = new EquipoPresupuesto
                            {
                                Id = Guid.NewGuid(),
                                PresupuestoId = presupuestoId,
                                EquipoDepreciacionId = equipo.Id,
                                Codigo = item.Codigo,
                                Descripcion = item.Descripcion,
                                Unidad = item.Unidad,
                                Cantidad = item.Cantidad,
                                ValorUnitario = item.ValorUnitario,
                                ValorTotal = item.ValorTotal,
                                Rubro = item.Rubro ?? string.Empty,
                                Observaciones = item.Observaciones
                            };
                            _context.EquipoPresupuesto.Add(eqPres);
                            await _context.SaveChangesAsync();
                            break;

                        case "Servicios a Terceros":
                            var servicio = await GetOrCreateServicioTerceroAsync(
                                nombre: item.Descripcion,
                                costoUnitario: item.ValorUnitario,
                                empresaId: proyecto.EmpresaId
                            );
                            var servPres = new ServicioTerceroPresupuesto
                            {
                                Id = Guid.NewGuid(),
                                PresupuestoId = presupuestoId,
                                ServicioTerceroId = servicio.Id,
                                Codigo = item.Codigo,
                                Descripcion = item.Descripcion,
                                Unidad = item.Unidad,
                                Cantidad = item.Cantidad,
                                ValorUnitario = item.ValorUnitario,
                                ValorTotal = item.ValorTotal,
                                Rubro = item.Rubro ?? string.Empty,
                                Observaciones = item.Observaciones
                            };
                            _context.ServicioTerceroPresupuesto.Add(servPres);
                            await _context.SaveChangesAsync();
                            break;

                        case "Costos Indirectos":
                            var costoInd = await GetOrCreateCostoIndirectoAsync(
                                nombre: item.Descripcion,
                                costoUnitario: item.ValorUnitario,
                                empresaId: proyecto.EmpresaId
                            );
                            var ciPres = new CostoIndirectoPresupuesto
                            {
                                Id = Guid.NewGuid(),
                                PresupuestoId = presupuestoId,
                                CostoIndirectoId = costoInd.Id,
                                Codigo = item.Codigo,
                                Descripcion = item.Descripcion,
                                Unidad = item.Unidad,
                                Cantidad = item.Cantidad,
                                ValorUnitario = item.ValorUnitario,
                                ValorTotal = item.ValorTotal,
                                Rubro = item.Rubro ?? string.Empty,
                                Observaciones = item.Observaciones
                            };
                            _context.CostoIndirectoPresupuesto.Add(ciPres);
                            break;

                        default:
                            throw new Exception($"Rubro inválido: {rubro}. Solo se permiten: Materiales, Mano de Obra, Equipos, Servicios a Terceros, Costos Indirectos.");
                    }
                }

                await transaction.CommitAsync();

                return Ok(new { ok = true, message = "Presupuesto y rubros importados correctamente" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                var inner = ex.InnerException != null ? ex.InnerException.Message : "";
                _logger.LogError(ex, $"Error al importar: {ex.Message} {inner}");
                return BadRequest(new { ok = false, message = $"Error al importar: {ex.Message} {inner}" });
            }
        }
        // Métodos GetOrCreate para cada maestra...

        private async Task<Material> GetOrCreateMaterialAsync(
            string nombre,
            decimal costoUnitario,
            int empresaId)
        {
            var material = await _context.Material.FirstOrDefaultAsync(m =>
                m.Nombre == nombre && m.EmpresaId == empresaId);

            if (material == null)
            {
                material = new Material
                {
                    Nombre = nombre,
                    CostoUnitario = costoUnitario,
                    EmpresaId = empresaId
                };
                _context.Material.Add(material);
                await _context.SaveChangesAsync();
            }
            else
            {
                if (material.CostoUnitario != costoUnitario)
                {
                    material.CostoUnitario = costoUnitario;
                    _context.Material.Update(material);
                    await _context.SaveChangesAsync();
                }
            }
            return material;
        }

        private async Task<ManoObra> GetOrCreateManoObraAsync(
            string descripcion,
            decimal costoHora,
            int empresaId,
            int tipoManoObraId)
        {
            var manoObra = await _context.ManosObra.FirstOrDefaultAsync(mo =>
                mo.Nombre == descripcion && mo.EmpresaId == empresaId && mo.TipoManoObraId == tipoManoObraId);

            if (manoObra == null)
            {
                manoObra = new ManoObra
                {
                    Descripcion = descripcion,
                    Nombre = descripcion,
                    CostoHora = costoHora,
                    EmpresaId = empresaId,
                    TipoManoObraId = tipoManoObraId
                };
                _context.ManosObra.Add(manoObra);
                await _context.SaveChangesAsync();
            }
            else
            {
                if (manoObra.CostoHora != costoHora || manoObra.TipoManoObraId != tipoManoObraId)
                {
                    manoObra.CostoHora = costoHora;
                    manoObra.TipoManoObraId = tipoManoObraId;
                    _context.ManosObra.Update(manoObra);
                    await _context.SaveChangesAsync();
                }
            }
            return manoObra;
        }

        private async Task<EquipoDepreciacion> GetOrCreateEquipoDepreciacionAsync(
            string nombreEquipo,
            decimal valorInicial,
            decimal valorResidual,
            int vidaUtilMeses,
            int empresaId)
        {
            var equipo = await _context.EquiposDepreciacion.FirstOrDefaultAsync(eq =>
                eq.NombreEquipo == nombreEquipo && eq.EmpresaId == empresaId);

            if (equipo == null)
            {
                equipo = new EquipoDepreciacion
                {
                    NombreEquipo = nombreEquipo,
                    ValorInicial = valorInicial,
                    ValorResidual = valorResidual,
                    VidaUtilMeses = vidaUtilMeses,
                    EmpresaId = empresaId
                };
                _context.EquiposDepreciacion.Add(equipo);
                await _context.SaveChangesAsync();
            }
            else
            {
                if (equipo.ValorInicial != valorInicial || equipo.ValorResidual != valorResidual || equipo.VidaUtilMeses != vidaUtilMeses)
                {
                    equipo.ValorInicial = valorInicial;
                    equipo.ValorResidual = valorResidual;
                    equipo.VidaUtilMeses = vidaUtilMeses;
                    _context.EquiposDepreciacion.Update(equipo);
                    await _context.SaveChangesAsync();
                }
            }
            return equipo;
        }

        private async Task<ServicioTercero> GetOrCreateServicioTerceroAsync(string nombre, decimal costoUnitario, int empresaId)
        {
            var servicio = await _context.ServicioTercero.FirstOrDefaultAsync(x => x.Nombre == nombre && x.EmpresaId == empresaId);
            if (servicio == null)
            {
                servicio = new ServicioTercero { Nombre = nombre, CostoUnitario = costoUnitario, EmpresaId = empresaId };
                _context.ServicioTercero.Add(servicio);
                await _context.SaveChangesAsync();
            }
            else if (servicio.CostoUnitario != costoUnitario)
            {
                servicio.CostoUnitario = costoUnitario;
                _context.ServicioTercero.Update(servicio);
                await _context.SaveChangesAsync();
            }
            return servicio;
        }

        private async Task<CostoIndirecto> GetOrCreateCostoIndirectoAsync(string nombre, decimal costoUnitario, int empresaId)
        {
            var costo = await _context.CostoIndirecto.FirstOrDefaultAsync(x => x.Nombre == nombre && x.EmpresaId == empresaId);
            if (costo == null)
            {
                costo = new CostoIndirecto { Nombre = nombre, CostoUnitario = costoUnitario, EmpresaId = empresaId };
                _context.CostoIndirecto.Add(costo);
                await _context.SaveChangesAsync();
            }
            else if (costo.CostoUnitario != costoUnitario)
            {
                costo.CostoUnitario = costoUnitario;
                _context.CostoIndirecto.Update(costo);
                await _context.SaveChangesAsync();
            }
            return costo;
        }
        [HttpPost("{presupuestoId}/costos-reales")]
        public async Task<IActionResult> GuardarCostosReales(int presupuestoId, [FromBody] PresupuestoDto dto)
        {
            var presupuesto = await _context.Presupuestos.FindAsync(presupuestoId);
            if (presupuesto == null)
                return NotFound();

            presupuesto.CostoRealMateriales = dto.CostoRealMateriales ?? 0;
            presupuesto.CostoRealManoObra = dto.CostoRealManoObra ?? 0;
            presupuesto.CostoRealEquipos = dto.CostoRealEquipos ?? 0;
            presupuesto.CostoRealServiciosTerceros = dto.CostoRealServiciosTerceros ?? 0;
            presupuesto.CostoRealCostosIndirectos = dto.CostoRealCostosIndirectos ?? 0;

            await _context.SaveChangesAsync();
            return Ok(new { ok = true });
        }

        [HttpGet("{presupuestoId}/items")]
        public async Task<IActionResult> GetItemsPorRubro(int presupuestoId, [FromQuery] string rubro)
        {
            var items = await _context.ItemPresupuesto
                .Where(x => x.PresupuestoId == presupuestoId && x.Rubro == rubro)
                .Include(x => x.EvidenciasItemPresupuesto) // si tienes la relación
                .Select(x => new {
                    id = x.Id,
                    codigo = x.Codigo,
                    descripcion = x.Descripcion ?? "No aplica",
                    unidad = x.Unidad,
                    cantidad = x.Cantidad,
                    valorPresupuestado = x.ValorUnitario,
                    valorTotal = x.ValorTotal,
                    valorReal = x.EvidenciasItemPresupuesto.Sum(e => e.ValorSoportado), // si tienes campo real, si no puedes sumar evidencias
                    evidencias = x.EvidenciasItemPresupuesto.Select(e => new {
                        e.Id,
                        e.NombreArchivo,
                        e.UrlArchivo,
                        e.TipoArchivo
                    }).ToList()
                })
                .ToListAsync();

            return Ok(new { ok = true, contenido = items });
        }

        [HttpPost("{presupuestoId}/item/{itemId}/evidencias")]
        public async Task<IActionResult> CargarEvidencia(int presupuestoId, Guid itemId)
        {
            var item = await _context.ItemPresupuesto
                .FirstOrDefaultAsync(x => x.Id == itemId && x.PresupuestoId == presupuestoId);

            if (item == null)
                return NotFound(new { ok = false, message = "Ítem no encontrado" });

            var files = Request.Form.Files;
            if (files.Count == 0)
                return BadRequest(new { ok = false, message = "Ningún archivo recibido" });

            decimal valorSoportado = 0;
            decimal.TryParse(Request.Form["valorSoportado"], out valorSoportado);

            var evidencias = new List<EvidenciaItemPresupuesto>();

            foreach (var file in files)
            {
                if (file.Length > 10 * 1024 * 1024)
                    return BadRequest(new { ok = false, message = "Archivo muy grande" });

                var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png" };
                var ext = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtensions.Contains(ext))
                    return BadRequest(new { ok = false, message = "Tipo de archivo no permitido" });

                var urlArchivo = await GuardarArchivoEnDiscoAsync(file);

                evidencias.Add(new EvidenciaItemPresupuesto
                {
                    Id = Guid.NewGuid(),
                    ItemPresupuestoId = itemId,
                    NombreArchivo = file.FileName,
                    UrlArchivo = urlArchivo,
                    TipoArchivo = file.ContentType,
                    ValorSoportado = valorSoportado,
                    FechaCarga = DateTime.UtcNow
                });
            }

            _context.EvidenciasItemPresupuesto.AddRange(evidencias);

            // Actualiza el costo real del ítem
            item.CostoReal = (await _context.EvidenciasItemPresupuesto
                .Where(e => e.ItemPresupuestoId == itemId)
                .SumAsync(e => (decimal?)e.ValorSoportado)) ?? 0;

            _context.ItemPresupuesto.Update(item);
            await _context.SaveChangesAsync();

            return Ok(new { ok = true, message = "Evidencia cargada correctamente", evidencias });
        }

        private async Task<string> GuardarArchivoEnDiscoAsync(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            var nombreArchivo = $"{Guid.NewGuid():N}{extension}";
            var rutaCompleta = Path.Combine(_carpetaEvidencias, nombreArchivo);

            // Asegura que la carpeta existe
            if (!Directory.Exists(_carpetaEvidencias))
                Directory.CreateDirectory(_carpetaEvidencias);

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Devuelve la URL relativa que luego tu frontend podrá consultar vía API
            return $"/uploads/evidencias/{nombreArchivo}";
        }


    }
}
