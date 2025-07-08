using System.Collections.Generic;
using System.Threading.Tasks;
using SafyroPresupuestos.Entities;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface IPermisoUsuarioService
    {
        Task<List<PermisoUsuario>> GetAllAsync();
        Task<PermisoUsuario> GetByIdAsync(int id);
        Task<PermisoUsuario> CreateAsync(PermisoUsuario entity);
        Task<PermisoUsuario> UpdateAsync(int id, PermisoUsuario entity);
        Task<bool> DeleteAsync(int id);
    }
}
