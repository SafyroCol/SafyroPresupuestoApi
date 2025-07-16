// Elimine o renombre esta clase, ya que ya existe otra definición de 'MaterialPresupuestoController' en el mismo espacio de nombres.
// Si necesita mantener ambas implementaciones, cambie el nombre de la clase o muévala a otro espacio de nombres.
// Ejemplo de renombrado:
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafyroPresupuestos.Data;
using SafyroPresupuestos.Entities;

namespace SafyroPresupuestos.Controllers
{
    [ApiController] // Eliminar o comentar esta línea para evitar el atributo duplicado
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class MatePresupuestoController : ControllerBase // Renombrado para evitar conflicto CS0101
    {
        private readonly ApplicationDbContext _context;

        public MatePresupuestoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MaterialPresupuesto/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<MaterialPresupuesto>> GetById(Guid id)
        {
            var materialPresupuesto = await _context.Set<MaterialPresupuesto>()
                .Include(mp => mp.Material)
                .Include(mp => mp.Presupuesto)
                .FirstOrDefaultAsync(mp => mp.Id == id);

            if (materialPresupuesto == null)
                return NotFound();

            return materialPresupuesto;
        }

        // GET: api/MaterialPresupuesto/ByPresupuesto/{presupuestoId}
        [HttpGet("ByPresupuesto/{presupuestoId:int}")]
        public async Task<ActionResult<IEnumerable<MaterialPresupuesto>>> GetByPresupuestoId(int presupuestoId)
        {
            if (presupuestoId <= 0)
                return BadRequest("El presupuestoId debe ser mayor a cero.");

            var materiales = await _context.Set<MaterialPresupuesto>()
                .Where(mp => mp.PresupuestoId == presupuestoId)
                .Include(mp => mp.Material)
                .ToListAsync();

            return Ok(materiales);
        }

    }
}