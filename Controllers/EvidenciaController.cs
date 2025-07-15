using Microsoft.AspNetCore.Mvc;
using SafyroPresupuestos.Data;
using SafyroPresupuestos.DTOs;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Entities;
using System;

namespace SafyroPresupuestos.Controllers
{
    [ApiController]
    [Route("api/evidencia")]
    public class EvidenciaController : ControllerBase
    {
        private readonly ApplicationDbContext _context; // Tu DbContext, ajusta el nombre

        public EvidenciaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("upload")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadEvidencia(
            [FromForm] int proyectoId,
            [FromForm] List<IFormFile> files,
            [FromForm] string comentario = null)
        {
            if (files == null || files.Count == 0)
                return BadRequest(new { ok = false, message = "No hay archivos para subir" });

            var urls = new List<string>();
            var evidencias = new List<Evidencia>();
            var folder = Path.Combine("wwwroot", "evidencias", proyectoId.ToString());
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            foreach (var file in files)
            {
                var evidenciaId = Guid.NewGuid();
                var fileName = $"{evidenciaId}_{Path.GetFileName(file.FileName)}";
                var filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                var url = $"/evidencias/{proyectoId}/{fileName}";

                evidencias.Add(new Evidencia
                {
                    Id = evidenciaId,
                    ProyectoId = proyectoId,
                    FileUrl = url,
                    FileName = file.FileName,
                    Comentario = comentario, // Se guarda el comentario aquí
                    FechaSubida = DateTime.UtcNow,
                    UsuarioId = User?.Identity?.Name ?? "anon"
                });
            }

            // Guardar en la base de datos
            _context.Evidencias.AddRange(evidencias);
            await _context.SaveChangesAsync();

            var result = evidencias.Select(e => new EvidenciaDto
            {
                Id = e.Id,
                FileUrl = e.FileUrl,
                FileName = e.FileName,
                Comentario = e.Comentario,
                FechaSubida = e.FechaSubida,
                UsuarioId = e.UsuarioId
            }).ToList();

            return Ok(new { ok = true, evidencias = result });
        }


        [HttpGet("por-proyecto/{proyectoId}")]
        public IActionResult GetEvidenciasPorProyecto(int proyectoId)
        {
            var evidencias = _context.Evidencias
                .Where(e => e.ProyectoId == proyectoId)
                .OrderByDescending(e => e.FechaSubida)
                .Select(e => new EvidenciaDto
                {
                    Id = e.Id,
                    FileUrl = e.FileUrl,
                    FileName = e.FileName,
                    Comentario = e.Comentario,
                    FechaSubida = e.FechaSubida,
                    UsuarioId = e.UsuarioId
                })
                .ToList();

            return Ok(new { ok = true, evidencias });
        }

        [HttpPatch("comentario")]
        public async Task<IActionResult> ActualizarComentarioEvidencia([FromBody] EvidenciaComentarioUpdateDto dto)
        {
            if (dto == null || dto.EvidenciaId == Guid.Empty)
                return BadRequest(new { ok = false, message = "Datos inválidos." });

            var evidencia = await _context.Evidencias.FindAsync(dto.EvidenciaId);
            if (evidencia == null)
                return NotFound(new { ok = false, message = "Evidencia no encontrada." });

            evidencia.Comentario = dto.Comentario;
            await _context.SaveChangesAsync();

            return Ok(new { ok = true, comentario = evidencia.Comentario });
        }

    }

}
