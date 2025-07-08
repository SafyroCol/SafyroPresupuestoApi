
using SafyroPresupuestos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface IMaterialPresupuestoService
    {
        Task<IEnumerable<MaterialPresupuesto>> GetAllAsync();
        Task<MaterialPresupuesto> GetByIdAsync(Guid id);
        Task CreateAsync(MaterialPresupuesto item);
        Task UpdateAsync(MaterialPresupuesto item);
        Task DeleteAsync(Guid id);
    }
}
