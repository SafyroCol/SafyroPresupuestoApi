using SafyroPresupuestos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface IEquipoDepreciacionService
    {
        Task<List<EquipoDepreciacion>> GetAll();
        Task<EquipoDepreciacion> GetById(int id);
        Task<EquipoDepreciacion> Create(EquipoDepreciacion equipo);
        Task<EquipoDepreciacion> Update(int id, EquipoDepreciacion equipo);
        Task<bool> Delete(int id);
    }
}