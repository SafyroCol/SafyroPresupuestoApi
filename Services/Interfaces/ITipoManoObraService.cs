using System.Collections.Generic;
using System.Threading.Tasks;
using SafyroPresupuestos.Entities;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface ITipoManoObraService
    {
        Task<List<TipoManoObra>> GetAllAsync();
        Task<TipoManoObra> GetByIdAsync(int id);
        Task<TipoManoObra> CreateAsync(TipoManoObra entity);
        Task<TipoManoObra> UpdateAsync(int id, TipoManoObra entity);
        Task<bool> DeleteAsync(int id);
    }
}
