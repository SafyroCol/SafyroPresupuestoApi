using System.Collections.Generic;
using System.Threading.Tasks;
using SafyroPresupuestos.Entities;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface IProyectoService
    {
        Task<List<Proyecto>> GetAllAsync();
        Task<Proyecto> GetByIdAsync(int id);
        Task<Proyecto> CreateAsync(Proyecto entity);
        Task<Proyecto> UpdateAsync(int id, Proyecto entity);
        Task<bool> DeleteAsync(int id);
    }
}
