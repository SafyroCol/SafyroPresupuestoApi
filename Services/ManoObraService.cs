using SafyroPresupuestos.Data;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafyroPresupuestos.Services
{
    public class ManoObraService : IManoObraService
    {
        private readonly ApplicationDbContext _context;

        public ManoObraService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ManoObra>> GetAll() => await _context.ManosObra.ToListAsync();
        public async Task<ManoObra> GetById(int id) => await _context.ManosObra.FindAsync(id);
        public async Task<ManoObra> Create(ManoObra manoObra)
        {
            _context.ManosObra.Add(manoObra);
            await _context.SaveChangesAsync();
            return manoObra;
        }
        public async Task<ManoObra> Update(int id, ManoObra manoObra)
        {
            var existing = await _context.ManosObra.FindAsync(id);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(manoObra);
            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<bool> Delete(int id)
        {
            var manoObra = await _context.ManosObra.FindAsync(id);
            if (manoObra == null) return false;
            _context.ManosObra.Remove(manoObra);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}