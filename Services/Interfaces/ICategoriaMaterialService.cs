using System.Collections.Generic;
using System.Threading.Tasks;
using SafyroPresupuestos.Entities;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface ICategoriaMaterialService
    {
        Task<List<CategoriaMaterial>> GetAllAsync();
        Task<CategoriaMaterial> GetByIdAsync(int id);
        Task<CategoriaMaterial> CreateAsync(CategoriaMaterial entity);
        Task<CategoriaMaterial> UpdateAsync(int id, CategoriaMaterial entity);
        Task<bool> DeleteAsync(int id);
    }
}
