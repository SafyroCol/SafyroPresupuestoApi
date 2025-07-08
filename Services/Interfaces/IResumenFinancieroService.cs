using System.Collections.Generic;
using System.Threading.Tasks;
using SafyroPresupuestos.Entities;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface IResumenFinancieroService
    {
        Task<List<ResumenFinanciero>> GetAllAsync();
        Task<ResumenFinanciero> GetByIdAsync(int id);
        Task<ResumenFinanciero> CreateAsync(ResumenFinanciero entity);
        Task<ResumenFinanciero> UpdateAsync(int id, ResumenFinanciero entity);
        Task<bool> DeleteAsync(int id);
    }
}
