using System.Collections.Generic;
using System.Threading.Tasks;
using SafyroPresupuestos.Entities;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface ITipoEquipoService
    {
        Task<List<TipoEquipo>> GetAllAsync();
        Task<TipoEquipo> GetByIdAsync(int id);
        Task<TipoEquipo> CreateAsync(TipoEquipo entity);
        Task<TipoEquipo> UpdateAsync(int id, TipoEquipo entity);
        Task<bool> DeleteAsync(int id);
    }
}
