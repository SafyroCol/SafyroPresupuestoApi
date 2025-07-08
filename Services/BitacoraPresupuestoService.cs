// Services/BitacoraPresupuestoService.cs
using Microsoft.EntityFrameworkCore;
using SafyroPresupuestos.Data;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;

namespace SafyroPresupuestos.Services
{
    public class BitacoraPresupuestoService : IBitacoraPresupuestoService
    {
        private readonly ApplicationDbContext _context;

        public BitacoraPresupuestoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BitacoraPresupuesto>> GetAllAsync() =>
            await _context.BitacorasPresupuesto.ToListAsync();

        public async Task<BitacoraPresupuesto> GetByIdAsync(int id) =>
            await _context.BitacorasPresupuesto.FindAsync(id);

        public async Task<BitacoraPresupuesto> CreateAsync(BitacoraPresupuesto item)
        {
            _context.BitacorasPresupuesto.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<BitacoraPresupuesto> UpdateAsync(BitacoraPresupuesto item)
        {
            _context.BitacorasPresupuesto.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(int id) // 👈 Implementación
        {
            var entity = await _context.BitacorasPresupuesto.FindAsync(id);
            if (entity == null) return false;

            _context.BitacorasPresupuesto.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
