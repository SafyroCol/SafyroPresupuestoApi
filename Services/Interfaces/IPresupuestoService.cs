using SafyroPresupuestos.DTOs;
using SafyroPresupuestos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface IPresupuestoService
    {
		Task<List<Presupuestos>> GetAll();
        Task<Presupuestos> GetById(int id);
        Task<Presupuestos> Create(Presupuestos presupuesto);
        Task<Presupuestos> Update(int id, Presupuestos presupuesto);
        Task<bool> Delete(int id);
        Task<int> ObtenerEmpresaPorPresupuesto(int presupuestoId);
        Task<List<MaterialConCostoDto>> ObtenerMaterialesConCostosAsync(int presupuestoId);
        Task<ResponseDto<object>> RegistrarCostosRealesAsync(CostosRealesMaterialDto dto);
        Task<PresupuestoDto> ObtenerPorProyectoIdAsync(int proyectoId);


    }
}