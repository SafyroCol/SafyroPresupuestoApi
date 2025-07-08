using SafyroPresupuestos.DTOs;
using SafyroPresupuestos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface IMaterialService
    {
        Task<List<Material>> GetAll();
        Task<Material> GetById(int id);
        Task<Material> Create(Material material);
        Task<Material> Update(int id, Material material);
        Task<bool> Delete(int id);

        Task<List<MaterialCostoDto>> ObtenerMaterialesConCostosAsync(int presupuestoId);

    }
}