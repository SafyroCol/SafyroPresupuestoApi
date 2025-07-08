using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafyroPresupuestos.Data;
using SafyroPresupuestos.DTOs;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;

namespace SafyroPresupuestos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProyectoController : ControllerBase
    {
        private readonly IProyectoService _proyectoService;
        private readonly ApplicationDbContext _context;

        public ProyectoController(IProyectoService proyectoService, ApplicationDbContext context)
        {
            _proyectoService = proyectoService;
            _context = context;
        }

        // GET paginado
        [HttpGet]
        public async Task<ActionResult<ResponseDto<IEnumerable<ProyectoResponseDto>>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var query = _context.Proyectos.Include(p => p.Empresa).AsQueryable();

                var total = await query.CountAsync();
                var proyectos = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(p => new ProyectoResponseDto
                    {
                        Id = p.Id,
                        Nombre = p.Nombre,
                        EmpresaId = p.EmpresaId,
                        EmpresaNombre = p.Empresa != null ? p.Empresa.Nombre : string.Empty
                    })
                    .ToListAsync();

                return Ok(new ResponseDto<IEnumerable<ProyectoResponseDto>>
                {
                    Ok = true,
                    Mensaje = proyectos.Any() ? "Lista de proyectos obtenida correctamente." : "No hay proyectos registrados.",
                    Contenido = proyectos,
                    Total = total,
                    Paginacion = new
                    {
                        PaginaActual = page,
                        TamanoPagina = pageSize,
                        TotalPaginas = (int)Math.Ceiling((double)total / pageSize)
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<string?>
                {
                    Ok = false,
                    Mensaje = $"Error al obtener proyectos: {ex.Message}",
                    Contenido = null
                });
            }
        }

        // GET por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<ProyectoResponseDto?>>> GetById(int id)
        {
            try
            {
                var proyecto = await _context.Proyectos
                    .Include(p => p.Empresa)
                    .Where(p => p.Id == id)
                    .Select(p => new ProyectoResponseDto
                    {
                        Id = p.Id,
                        Nombre = p.Nombre,
                        EmpresaId = p.EmpresaId,
                        EmpresaNombre = p.Empresa != null ? p.Empresa.Nombre : string.Empty
                    })
                    .FirstOrDefaultAsync();

                if (proyecto == null)
                {
                    return NotFound(new ResponseDto<ProyectoResponseDto?>
                    {
                        Ok = false,
                        Mensaje = $"No se encontró el proyecto con ID {id}.",
                        Contenido = null
                    });
                }

                return Ok(new ResponseDto<ProyectoResponseDto?>
                {
                    Ok = true,
                    Mensaje = "Proyecto obtenido correctamente.",
                    Contenido = proyecto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<string?>
                {
                    Ok = false,
                    Mensaje = $"Error al buscar el proyecto: {ex.Message}",
                    Contenido = null
                });
            }
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<ResponseDto<ProyectoResponseDto?>>> Create([FromBody] ProyectoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDto<object?>
                {
                    Ok = false,
                    Mensaje = "Datos inválidos.",
                    Contenido = ModelState
                });
            }

            try
            {
                var empresaExiste = await _context.Empresas.AnyAsync(e => e.Id == dto.EmpresaId);
                if (!empresaExiste)
                {
                    return BadRequest(new ResponseDto<string?>
                    {
                        Ok = false,
                        Mensaje = $"La empresa con ID {dto.EmpresaId} no existe.",
                        Contenido = null
                    });
                }

                var nuevo = new Proyecto
                {
                    Nombre = dto.Nombre,
                    EmpresaId = dto.EmpresaId
                };

                var creado = await _proyectoService.CreateAsync(nuevo);

                var dtoRespuesta = new ProyectoResponseDto
                {
                    Id = creado.Id,
                    Nombre = creado.Nombre,
                    EmpresaId = creado.EmpresaId,
                    EmpresaNombre = (await _context.Empresas.FindAsync(creado.EmpresaId))?.Nombre ?? string.Empty
                };

                return CreatedAtAction(nameof(GetById), new { id = dtoRespuesta.Id }, new ResponseDto<ProyectoResponseDto?>
                {
                    Ok = true,
                    Mensaje = "Proyecto creado correctamente.",
                    Contenido = dtoRespuesta
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<string?>
                {
                    Ok = false,
                    Mensaje = $"Error al crear el proyecto: {ex.Message}",
                    Contenido = null
                });
            }
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<ProyectoResponseDto?>>> Update(int id, [FromBody] ProyectoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDto<object?>
                {
                    Ok = false,
                    Mensaje = "Datos inválidos.",
                    Contenido = ModelState
                });
            }

            try
            {
                var empresaExiste = await _context.Empresas.AnyAsync(e => e.Id == dto.EmpresaId);
                if (!empresaExiste)
                {
                    return BadRequest(new ResponseDto<string?>
                    {
                        Ok = false,
                        Mensaje = $"La empresa con ID {dto.EmpresaId} no existe.",
                        Contenido = null
                    });
                }

                var entidad = new Proyecto
                {
                    Id = id,
                    Nombre = dto.Nombre,
                    EmpresaId = dto.EmpresaId
                };

                var actualizado = await _proyectoService.UpdateAsync(id, entidad);
                if (actualizado == null)
                {
                    return NotFound(new ResponseDto<ProyectoResponseDto?>
                    {
                        Ok = false,
                        Mensaje = $"No se encontró el proyecto con ID {id} para actualizar.",
                        Contenido = null
                    });
                }

                var dtoRespuesta = new ProyectoResponseDto
                {
                    Id = actualizado.Id,
                    Nombre = actualizado.Nombre,
                    EmpresaId = actualizado.EmpresaId,
                    EmpresaNombre = (await _context.Empresas.FindAsync(actualizado.EmpresaId))?.Nombre ?? string.Empty
                };

                return Ok(new ResponseDto<ProyectoResponseDto?>
                {
                    Ok = true,
                    Mensaje = "Proyecto actualizado correctamente.",
                    Contenido = dtoRespuesta
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<string?>
                {
                    Ok = false,
                    Mensaje = $"Error al actualizar el proyecto: {ex.Message}",
                    Contenido = null
                });
            }
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<string?>>> Delete(int id)
        {
            try
            {
                var eliminado = await _proyectoService.DeleteAsync(id);
                if (!eliminado)
                {
                    return NotFound(new ResponseDto<string?>
                    {
                        Ok = false,
                        Mensaje = $"No se encontró el proyecto con ID {id} para eliminar.",
                        Contenido = null
                    });
                }

                return Ok(new ResponseDto<string?>
                {
                    Ok = true,
                    Mensaje = $"Proyecto con ID {id} eliminado correctamente.",
                    Contenido = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<string?>
                {
                    Ok = false,
                    Mensaje = $"Error al eliminar el proyecto: {ex.Message}",
                    Contenido = null
                });
            }
        }
    }
}
