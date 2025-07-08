
using SafyroPresupuestos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface IEquipoPresupuestoService
    {
        Task<IEnumerable<EquipoPresupuesto>> GetAllAsync();
        Task<EquipoPresupuesto> GetByIdAsync(Guid id);
        Task CreateAsync(EquipoPresupuesto item);
        Task UpdateAsync(EquipoPresupuesto item);
        Task DeleteAsync(Guid id);
    }
}
