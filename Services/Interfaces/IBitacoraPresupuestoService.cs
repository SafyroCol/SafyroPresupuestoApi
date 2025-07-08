// Interfaces/IBitacoraPresupuestoService.cs
using SafyroPresupuestos.Entities;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface IBitacoraPresupuestoService
    {
        Task<IEnumerable<BitacoraPresupuesto>> GetAllAsync();
        Task<BitacoraPresupuesto> GetByIdAsync(int id);
        Task<BitacoraPresupuesto> CreateAsync(BitacoraPresupuesto item);
        Task<BitacoraPresupuesto> UpdateAsync(BitacoraPresupuesto item);
        Task<bool> DeleteAsync(int id); // 👈 Añadir esta firma
    }
}
