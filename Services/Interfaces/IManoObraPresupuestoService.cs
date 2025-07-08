
using SafyroPresupuestos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface IManoObraPresupuestoService
    {
        Task<IEnumerable<ManoObraPresupuesto>> GetAllAsync();
        Task<ManoObraPresupuesto> GetByIdAsync(int id);
        Task CreateAsync(ManoObraPresupuesto item);
        Task UpdateAsync(ManoObraPresupuesto item);
        Task DeleteAsync(int id);
    }
}
