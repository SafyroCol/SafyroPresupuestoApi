using System.Collections.Generic;
using System.Threading.Tasks;
using SafyroPresupuestos.Data;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SafyroPresupuestos.Services
{
    public class CategoriaMaterialService : ICategoriaMaterialService
    {
        private readonly ApplicationDbContext _context;

        public CategoriaMaterialService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoriaMaterial>> GetAllAsync()
        {
            return await _context.Set<CategoriaMaterial>().ToListAsync();
        }

        public async Task<CategoriaMaterial> GetByIdAsync(int id)
        {
            return await _context.Set<CategoriaMaterial>().FindAsync(id);
        }

        public async Task<CategoriaMaterial> CreateAsync(CategoriaMaterial entity)
        {
            _context.Set<CategoriaMaterial>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<CategoriaMaterial> UpdateAsync(int id, CategoriaMaterial entity)
        {
            var existing = await _context.Set<CategoriaMaterial>().FindAsync(id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<CategoriaMaterial>().FindAsync(id);
            if (entity == null) return false;

            _context.Set<CategoriaMaterial>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
