using System.Collections.Generic;
using System.Threading.Tasks;
using SafyroPresupuestos.Data;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SafyroPresupuestos.Services
{
    public class TipoManoObraService : ITipoManoObraService
    {
        private readonly ApplicationDbContext _context;

        public TipoManoObraService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TipoManoObra>> GetAllAsync()
        {
            return await _context.Set<TipoManoObra>().ToListAsync();
        }

        public async Task<TipoManoObra> GetByIdAsync(int id)
        {
            return await _context.Set<TipoManoObra>().FindAsync(id);
        }

        public async Task<TipoManoObra> CreateAsync(TipoManoObra entity)
        {
            _context.Set<TipoManoObra>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TipoManoObra> UpdateAsync(int id, TipoManoObra entity)
        {
            var existing = await _context.Set<TipoManoObra>().FindAsync(id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<TipoManoObra>().FindAsync(id);
            if (entity == null) return false;

            _context.Set<TipoManoObra>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
