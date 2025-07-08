using SafyroPresupuestos.Data;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafyroPresupuestos.Services
{
    public class EquipoDepreciacionService : IEquipoDepreciacionService
    {
        private readonly ApplicationDbContext _context;

        public EquipoDepreciacionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EquipoDepreciacion>> GetAll() => await _context.EquiposDepreciacion.ToListAsync();
        public async Task<EquipoDepreciacion> GetById(int id) => await _context.EquiposDepreciacion.FindAsync(id);
        public async Task<EquipoDepreciacion> Create(EquipoDepreciacion equipo)
        {
            _context.EquiposDepreciacion.Add(equipo);
            await _context.SaveChangesAsync();
            return equipo;
        }
        public async Task<EquipoDepreciacion> Update(int id, EquipoDepreciacion equipo)
        {
            var existing = await _context.EquiposDepreciacion.FindAsync(id);
            if (existing == null) return null;
            _context.EquiposDepreciacion.Entry(existing).CurrentValues.SetValues(equipo);
            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<bool> Delete(int id)
        {
            var equipo = await _context.EquiposDepreciacion.FindAsync(id);
            if (equipo == null) return false;
            _context.EquiposDepreciacion.Remove(equipo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}