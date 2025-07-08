using System.Collections.Generic;
using System.Threading.Tasks;
using SafyroPresupuestos.Data;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SafyroPresupuestos.Services
{
    public class PermisoUsuarioService : IPermisoUsuarioService
    {
        private readonly ApplicationDbContext _context;

        public PermisoUsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PermisoUsuario>> GetAllAsync()
        {
            return await _context.Set<PermisoUsuario>().ToListAsync();
        }

        public async Task<PermisoUsuario> GetByIdAsync(int id)
        {
            return await _context.Set<PermisoUsuario>().FindAsync(id);
        }

        public async Task<PermisoUsuario> CreateAsync(PermisoUsuario entity)
        {
            _context.Set<PermisoUsuario>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<PermisoUsuario> UpdateAsync(int id, PermisoUsuario entity)
        {
            var existing = await _context.Set<PermisoUsuario>().FindAsync(id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<PermisoUsuario>().FindAsync(id);
            if (entity == null) return false;

            _context.Set<PermisoUsuario>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
