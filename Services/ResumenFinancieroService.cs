using System.Collections.Generic;
using System.Threading.Tasks;
using SafyroPresupuestos.Data;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SafyroPresupuestos.Services
{
    public class ResumenFinancieroService : IResumenFinancieroService
    {
        private readonly ApplicationDbContext _context;

        public ResumenFinancieroService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ResumenFinanciero>> GetAllAsync()
        {
            return await _context.Set<ResumenFinanciero>().ToListAsync();
        }

        public async Task<ResumenFinanciero> GetByIdAsync(int id)
        {
            return await _context.Set<ResumenFinanciero>().FindAsync(id);
        }

        public async Task<ResumenFinanciero> CreateAsync(ResumenFinanciero entity)
        {
            _context.Set<ResumenFinanciero>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ResumenFinanciero> UpdateAsync(int id, ResumenFinanciero entity)
        {
            var existing = await _context.Set<ResumenFinanciero>().FindAsync(id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<ResumenFinanciero>().FindAsync(id);
            if (entity == null) return false;

            _context.Set<ResumenFinanciero>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
