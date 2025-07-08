using System.Collections.Generic;
using System.Threading.Tasks;
using SafyroPresupuestos.Data;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SafyroPresupuestos.Services
{
    public class TipoEquipoService : ITipoEquipoService
    {
        private readonly ApplicationDbContext _context;

        public TipoEquipoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TipoEquipo>> GetAllAsync()
        {
            return await _context.Set<TipoEquipo>().ToListAsync();
        }

        public async Task<TipoEquipo> GetByIdAsync(int id)
        {
            return await _context.Set<TipoEquipo>().FindAsync(id);
        }

        public async Task<TipoEquipo> CreateAsync(TipoEquipo entity)
        {
            _context.Set<TipoEquipo>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TipoEquipo> UpdateAsync(int id, TipoEquipo entity)
        {
            var existing = await _context.Set<TipoEquipo>().FindAsync(id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<TipoEquipo>().FindAsync(id);
            if (entity == null) return false;

            _context.Set<TipoEquipo>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
