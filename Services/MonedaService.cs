using System.Collections.Generic;
using System.Threading.Tasks;
using SafyroPresupuestos.Data;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SafyroPresupuestos.Services
{
    public class MonedaService : IMonedaService
    {
        private readonly ApplicationDbContext _context;

        public MonedaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Moneda>> GetAllAsync()
        {
            return await _context.Set<Moneda>().ToListAsync();
        }

        public async Task<Moneda> GetByIdAsync(int id)
        {
            return await _context.Set<Moneda>().FindAsync(id);
        }

        public async Task<Moneda> CreateAsync(Moneda entity)
        {
            _context.Set<Moneda>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Moneda> UpdateAsync(int id, Moneda entity)
        {
            var existing = await _context.Set<Moneda>().FindAsync(id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<Moneda>().FindAsync(id);
            if (entity == null) return false;

            _context.Set<Moneda>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
