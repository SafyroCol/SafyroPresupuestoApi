using System.Collections.Generic;
using System.Threading.Tasks;
using SafyroPresupuestos.Data;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SafyroPresupuestos.Services
{
    public class ProyectoService : IProyectoService
    {
        private readonly ApplicationDbContext _context;

        public ProyectoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Proyecto>> GetAllAsync()
        {
            return await _context.Set<Proyecto>().ToListAsync();
        }

        public async Task<Proyecto> GetByIdAsync(int id)
        {
            return await _context.Set<Proyecto>().FindAsync(id);
        }

        public async Task<Proyecto> CreateAsync(Proyecto entity)
        {
            _context.Set<Proyecto>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Proyecto> UpdateAsync(int id, Proyecto entity)
        {
            var existing = await _context.Set<Proyecto>().FindAsync(id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<Proyecto>().FindAsync(id);
            if (entity == null) return false;

            _context.Set<Proyecto>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
