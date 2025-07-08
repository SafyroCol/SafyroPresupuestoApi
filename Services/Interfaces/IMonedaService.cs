using System.Collections.Generic;
using System.Threading.Tasks;
using SafyroPresupuestos.Entities;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface IMonedaService
    {
        Task<List<Moneda>> GetAllAsync();
        Task<Moneda> GetByIdAsync(int id);
        Task<Moneda> CreateAsync(Moneda entity);
        Task<Moneda> UpdateAsync(int id, Moneda entity);
        Task<bool> DeleteAsync(int id);
    }
}
