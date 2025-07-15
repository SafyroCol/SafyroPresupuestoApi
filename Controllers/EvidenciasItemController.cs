using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafyroPresupuestos.Data;
using SafyroPresupuestos.DTOs;
using SafyroPresupuestos.Entities;

namespace SafyroPresupuestos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class EvidenciasItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EvidenciasItemController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost("cargar")]
        public async Task<IActionResult> CargarEvidencia([FromForm] CargarEvidenciaDto dto)
        {
            var item = await _context.ItemPresupuesto.FindAsync(dto.ItemId);
            if (item == null) return BadRequest("Ítem no existe");

            // Guardar archivo físico (ajusta la ruta a tu gusto)
            string nombreArchivo = $"{Guid.NewGuid()}_{dto.Archivo.FileName}";
            string carpeta = Path.Combine(_env.WebRootPath, "evidencias");
            Directory.CreateDirectory(carpeta);
            string ruta = Path.Combine(carpeta, nombreArchivo);
            using (var stream = new FileStream(ruta, FileMode.Create))
            {
                await dto.Archivo.CopyToAsync(stream);
            }
            string url = $"/evidencias/{nombreArchivo}";

            // Guarda evidencia
            var evidencia = new EvidenciaItemPresupuesto
            {
                Id = Guid.NewGuid(),
                ItemPresupuestoId = dto.ItemId,
                NombreArchivo = dto.Archivo.FileName,
                UrlArchivo = url,
                FechaCarga = DateTime.UtcNow,
                ValorSoportado = dto.Costo                
            };
            _context.EvidenciasItemPresupuesto.Add(evidencia);

            // Suma el costo a un campo "CostoReal" en ItemPresupuesto
            item.CostoReal = (item.CostoReal ?? 0) + dto.Costo;

            await _context.SaveChangesAsync();
            return Ok(new { ok = true, evidencia });
        }

        [HttpGet("por-item/{itemId}")]
        public async Task<IActionResult> ListarEvidencias(Guid itemId)
        {
            var evidencias = await _context.EvidenciasItemPresupuesto
                .Where(e => e.ItemPresupuestoId == itemId)
                .ToListAsync();
            return Ok(evidencias);
        }
    }

}
