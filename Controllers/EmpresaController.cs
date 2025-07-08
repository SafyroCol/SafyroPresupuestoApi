using Microsoft.AspNetCore.Mvc;
using SafyroPresupuestos.DTOs;
using SafyroPresupuestos.Services.Interfaces;

namespace SafyroPresupuestos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;
        private readonly ILogger<EmpresaController> _logger;

        public EmpresaController(IEmpresaService empresaService, ILogger<EmpresaController> logger)
        {
            _empresaService = empresaService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<EmpresaDto>>>> GetAll(int page = 1, int pageSize = 10, string? search = null)
        {
            try
            {
                var (empresas, total) = await _empresaService.GetAllPagedAsync(page, pageSize, search);
                return Ok(new ResponseDto<List<EmpresaDto>>
                {
                    Ok = true,
                    Mensaje = empresas.Any() ? "Empresas obtenidas correctamente." : "No hay empresas registradas.",
                    Contenido = empresas,
                    Total = total,
                    Paginacion = new { page, pageSize, totalPages = (int)Math.Ceiling((double)total / pageSize) }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las empresas.");
                return StatusCode(500, new ResponseDto<List<EmpresaDto>>
                {
                    Ok = false,
                    Mensaje = "No se pudo recuperar la lista de empresas. Inténtelo más tarde.",
                    Contenido = null
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseDto<EmpresaDto?>>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest(new ResponseDto<EmpresaDto?>
                {
                    Ok = false,
                    Mensaje = "ID de empresa inválido.",
                    Contenido = null
                });

            try
            {
                var empresa = await _empresaService.GetByIdAsync(id);
                if (empresa is null)
                {
                    return NotFound(new ResponseDto<EmpresaDto?>
                    {
                        Ok = false,
                        Mensaje = "Empresa no encontrada.",
                        Contenido = null
                    });
                }

                return Ok(new ResponseDto<EmpresaDto?>
                {
                    Ok = true,
                    Mensaje = "Empresa encontrada.",
                    Contenido = empresa
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al consultar la empresa.");
                return StatusCode(500, new ResponseDto<EmpresaDto?>
                {
                    Ok = false,
                    Mensaje = "Error interno al consultar la empresa.",
                    Contenido = null
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<EmpresaDto>>> Create([FromBody] EmpresaCreateDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Nombre) || string.IsNullOrWhiteSpace(dto.Dominio))
                return BadRequest(new ResponseDto<EmpresaDto?>
                {
                    Ok = false,
                    Mensaje = "Complete los campos requeridos: Nombre y Dominio.",
                    Contenido = null
                });

            try
            {
                var created = await _empresaService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, new ResponseDto<EmpresaDto>
                {
                    Ok = true,
                    Mensaje = "Empresa creada exitosamente.",
                    Contenido = created
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la empresa.");
                return StatusCode(500, new ResponseDto<EmpresaDto?>
                {
                    Ok = false,
                    Mensaje = "Ocurrió un error al registrar la empresa. Por favor, revise los datos e intente nuevamente.",
                    Contenido = null
                });
            }
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<ResponseDto<EmpresaDto?>>> Update(int id, [FromBody] EmpresaDto dto)
        {
            if (id <= 0 || dto == null)
                return BadRequest(new ResponseDto<EmpresaDto?>
                {
                    Ok = false,
                    Mensaje = "Datos o ID inválidos.",
                    Contenido = null
                });

            try
            {
                var updated = await _empresaService.UpdateAsync(id, dto);
                if (updated is null)
                {
                    return NotFound(new ResponseDto<EmpresaDto?>
                    {
                        Ok = false,
                        Mensaje = "Empresa no encontrada.",
                        Contenido = null
                    });
                }

                return Ok(new ResponseDto<EmpresaDto?>
                {
                    Ok = true,
                    Mensaje = "Empresa actualizada correctamente.",
                    Contenido = updated
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la empresa.");
                return StatusCode(500, new ResponseDto<EmpresaDto?>
                {
                    Ok = false,
                    Mensaje = "Ocurrió un error al actualizar la empresa.",
                    Contenido = null
                });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ResponseDto<object?>>> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new ResponseDto<object?>
                {
                    Ok = false,
                    Mensaje = "ID de empresa inválido.",
                    Contenido = null
                });

            try
            {
                var deleted = await _empresaService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound(new ResponseDto<object?>
                    {
                        Ok = false,
                        Mensaje = "Empresa no encontrada.",
                        Contenido = null
                    });
                }

                return Ok(new ResponseDto<object?>
                {
                    Ok = true,
                    Mensaje = "Empresa eliminada correctamente.",
                    Contenido = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la empresa.");
                return StatusCode(500, new ResponseDto<object?>
                {
                    Ok = false,
                    Mensaje = "Ocurrió un error al eliminar la empresa.",
                    Contenido = null
                });
            }
        }
    }
}
